#region License
/* The MIT License
 *
 * Copyright (c) 2011 Red Badger Consulting
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
*/
#endregion

namespace Nine.Graphics.UI.Controls
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using Nine.Graphics.UI.Media;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Control to display a flow of Content
    /// </summary>
    public class TextBlock : UIElement
    {
        #region Fields

        private static readonly Regex WhiteSpaceRegEx = new Regex(@"\s+", RegexOptions.Compiled);

        public SpriteFont Font { get; set; }
        public SolidColorBrush Foreground { get; set; }
        public Thickness Padding { get; set; }
        public string Text { get; set; }
        public TextWrapping Wrapping { get; set; }

        private string formattedText;

        #endregion

        public TextBlock() { }
        public TextBlock(SpriteFont Font)
        {
            if (Font == null)
                throw new ArgumentNullException("spriteFont");

            this.Font = Font;
        }

        #region Methods

        protected internal override void OnRender(SpriteBatch spriteBatch)
        {
            base.OnRender(spriteBatch);

            var TextColor = this.Foreground ?? new SolidColorBrush(Color.Black);
            spriteBatch.DrawString(Font, formattedText, new Vector2(this.Padding.Left, this.Padding.Top) + AbsoluteVisualOffset, TextColor.ToColor());
        }

        protected override Vector2 ArrangeOverride(Vector2 finalSize)
        {
            return finalSize;
        }

        protected override Vector2 MeasureOverride(Vector2 availableSize)
        {
            this.formattedText = this.Text;
            Vector2 measureString = this.Font.MeasureString(this.formattedText);

            if (this.Wrapping == TextWrapping.Wrap && measureString.X > availableSize.X)
            {
                this.formattedText = WrapText(this.Font, this.formattedText, availableSize.X);
                measureString = this.Font.MeasureString(this.formattedText);
            }

            return new Vector2(
                measureString.X + this.Padding.Left + this.Padding.Right, 
                measureString.Y + this.Padding.Top + this.Padding.Bottom);
        }

        private static string WrapText(SpriteFont font, string text, float maxLineWidth)
        {
            const string Space = " ";
            var stringBuilder = new StringBuilder();
            string[] words = WhiteSpaceRegEx.Split(text);

            float lineWidth = 0;
            float spaceWidth = font.MeasureString(Space).X;

            foreach (string word in words)
            {
                Vector2 size = font.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    stringBuilder.AppendFormat("{0}{1}", lineWidth == 0 ? string.Empty : Space, word);
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    stringBuilder.AppendFormat("\n{0}", word);
                    lineWidth = size.X + spaceWidth;
                }
            }

            return stringBuilder.ToString();
        }

        #endregion
    }
}
