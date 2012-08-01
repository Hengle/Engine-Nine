﻿namespace Nine.Content.Pipeline.Importers
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.Xna.Framework.Content.Pipeline;
    using Nine.Content.Pipeline.Graphics;
    using Nine.Content.Pipeline.Xaml;

    /// <summary>
    /// Imports object graph from Xaml files.
    /// </summary>
    [ContentImporter(".xaml", DisplayName = "Xaml Importer - Engine Nine", DefaultProcessor="DefaultContentProcessor")]
    public class XamlImporter : ContentImporter<object>
    {
        ContentImporterContext context;

        public override object Import(string filename, ContentImporterContext context)
        {
            this.context = context;
            try
            {
                ContentProperties.IsContentBuild = true;

                var xamlSerializer = new XamlSerializer();
                xamlSerializer.InstanceResolve += new Func<Type, object[], object>(OnResolveInstance);
                return xamlSerializer.Load(Path.GetFullPath(filename));
            }
            catch (Exception e)
            {
                try
                {
                    // Sometimes this line will throw an exception if the InnerException is not in a correct format
                    // required by string.Format.
                    context.Logger.LogImportantMessage(e.InnerException.ToString());
                }
                catch { }
                throw;
            }
            finally
            {
                ContentProperties.IsContentBuild = false;
            }
        }

        private void AddDependencies(string filename, ContentImporterContext context, object result)
        {
            ObjectGraph.TraverseProperties(result, input =>
            {
                // Try to populate the identity of content items.
                var contentItem = input as ContentItem;
                if (contentItem != null)
                {
                    contentItem.Identity = new ContentIdentity(filename, typeof(XamlImporter).Name, null);
                }

                // Add dependencies to content references
                var inputType = input.GetType();
                if (inputType.IsGenericType && inputType.GetGenericTypeDefinition() == typeof(ContentReference<>))
                {
                    dynamic reference = input;
                    AddDependency(context, Path.GetDirectoryName(filename), reference.Filename);
                }
                return input;
            });
        }

        private void AddDependency(ContentImporterContext context, string directory, string filename)
        {
            // Try to guess the source file for the content reference
            if (Path.IsPathRooted(filename))
                throw new InvalidContentException("ContentReference has to be a relative path");

            filename = new Uri(Path.Combine(directory, filename)).LocalPath;
            directory = Path.GetDirectoryName(filename);
            filename = Path.GetFileName(filename);

            var dependenciesFound = false;
            if (Directory.Exists(directory))
            {
                foreach (var file in Directory.GetFiles(directory, filename + "*", SearchOption.TopDirectoryOnly))
                {
                    context.Logger.LogImportantMessage("Adding dependency {0}", file);
                    context.AddDependency(file);
                    dependenciesFound = true;
                }
            }

            if (!dependenciesFound)
            {
                context.Logger.LogWarning(null, null, "Content reference source asset not found for {0}", filename);
            }
        }

        /// <summary>
        /// Called when this xaml importer failed to create an instance of the specified type and arguments.
        /// </summary>
        protected virtual object OnResolveInstance(Type type, object[] arguments)
        {
            try
            {
                try
                {
                    // Enable internal constructors.
                    return Activator.CreateInstance(type, true);
                }
                catch (MissingMethodException)
                {
                    // Enable constructors that takes a graphics device.
                    return Activator.CreateInstance(type, new object[] { PipelineGraphics.GraphicsDevice });
                }
            }
            catch (TargetInvocationException ex)
            {
                context.Logger.LogWarning(null, null, "{0}", ex.InnerException);
                throw;
            }
        }
    }
}