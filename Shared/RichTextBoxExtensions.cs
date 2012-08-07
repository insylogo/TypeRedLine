using System.Windows.Forms;
using System.Drawing;

namespace Shared
{
    /// <summary>
    /// Contains extensions for RichTextBox to make the game function.
    /// </summary>
    public static class RichTextBoxExtensions
    {

        /// <summary>
        /// Gets the current column the cursor is on.
        /// </summary>
        /// <param name="rtb">The RichTextBox.</param>
        public static int CurrentColumn(this RichTextBox rtb)
        {
            return rtb.Column(rtb.SelectionStart); 
        }

        /// <summary>
        /// Gets the current line the cursor is on.
        /// </summary>
        /// <param name="rtb">The RichTextBox.</param>
        public static int CurrentLine(this RichTextBox rtb)
        {
            return rtb.Line(rtb.SelectionStart ); 
        }


        /// <summary>
        /// Gets the current position of the cursor.
        /// </summary>
        /// <param name="rtb">The RichTextBox.</param>
        /// <returns></returns>
        public static int GetCurrentPosition(this RichTextBox rtb)
        {
            return rtb.SelectionStart;
        }

        /// <summary>
        /// Gets the location of the end of the selection.
        /// </summary>
        /// <param name="rtb">The RichTextBox.</param>
        /// <returns></returns>
        public static int SelectionEnd(this RichTextBox rtb)
        {
            return rtb.SelectionStart + rtb.SelectionLength; 
        }

        /// <summary>
        /// Gets the caret pos.
        /// </summary>
        /// <param name="lpPoint">The point to populate with the Caret Position.</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32")] 
        public static extern int GetCaretPos(ref Point lpPoint);

        /// <summary>
        /// Gets the correction for the caret.
        /// </summary>
        /// <param name="e">The richTextBox.</param>
        /// <param name="index">The index of the character.</param>
        /// <returns></returns>
        private static int GetCorrection(this RichTextBox e, int index)
        {
            Point pt1 = Point.Empty;
            GetCaretPos(ref pt1);
            Point pt2 = e.GetPositionFromCharIndex(index);

            if ( pt1 != pt2 )
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// Gets the line number based on the character index.
        /// </summary>
        /// <param name="e">The text box.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static int Line(this RichTextBox e, int index )
        {
             return e.GetLineFromCharIndex( index );
        }

        /// <summary>
        /// Gets the column based on the character index.
        /// </summary>
        /// <param name="e">The rich text box.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static int Column(this RichTextBox e, int index)
        {
             int correction = GetCorrection( e, index );
             Point p = e.GetPositionFromCharIndex( index - correction );

             if ( p.X == 1 )
                 return 1;

             p.X = 0;
             int index2 = e.GetCharIndexFromPosition( p );

             int col = index - index2 + 1;

             return col;
         }
    }
}
