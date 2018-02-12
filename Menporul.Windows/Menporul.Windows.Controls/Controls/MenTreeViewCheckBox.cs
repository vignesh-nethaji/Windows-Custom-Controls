using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Menporul.Windows.Controls.Core;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// MenTreeViewCheckBox is Custom Control for Treeview with Checkbox .It has Override the default treeview and its make its has rich treeview based on the Design Principle. 
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenTreeViewCheckBox : TreeView
    {
        /// <summary>
        /// This node is used to store before selecte node on the Event of BeforeSelect.
        /// Based on this node Expand and Collapse Will occur. 
        /// </summary>        
        TreeNode beforeNode;
        /// <summary>
        /// This brush is used to set the Background Color on Selected Node.
        /// </summary>
        Brush _selectedBackColorBsh = new SolidBrush(MenConstants.TreeViewSelectedBackGround);

        /// <summary>
        /// This brush is used to set the Tree node Text ForeGround Color.
        /// </summary>
        Brush _textBsh = new SolidBrush(MenConstants.TreeViewText);
        /// <summary>
        /// This brush is used to set the Checkbox Ground Color.
        /// </summary>
        Brush _bshCheckBoxBackGround = new SolidBrush(MenConstants.Light);
        /// <summary>
        /// This brush is used to set the Checkbox Border Color.
        /// </summary>
        Pen _penCheckBoxBorder = new Pen(MenConstants.TreeViewCheckBoxBorder);
        /// <summary>
        /// This Image is used to draw the checkbox Checkmark.
        /// </summary>
        private Image _imgCheckMark = global::Menporul.Windows.Controls.MenResource.tick;
        /// <summary>
        /// Set the Default Height of the Treeview Nodes
        /// </summary>
        private int _defaultNodeHeight = 16;
        /// <summary>
        /// define the checkbox width and Height
        /// </summary>
        private int _checkBoxSize = 12;
        /// <summary>
        /// Constructor initialize and set the Particular Property values for Custom Design 
        /// </summary>
        /// <param name="Font">Set the Custom Design Font For TreeView</param>
        /// <param name="ItemHeight">set the Custom ItemHeight for Treeview.It's represents the space between two nodes.</param>
        /// <param name="ShowPlusMinus">set False for this property for remove the Plus/Minus Icon on treenodes </param>
        /// <param name="ShowRootLines">set false for this property for remove the default lines on treeview nodes</param>
        /// <param name="ShowNodeToolTips">set treu for this property for view the ToolTip on treenode MouseOver</param>
        /// <param name="DrawMode">Set OwnerDrawnAll for Draw Custom TreeView based on our Design Standards</param>
        public MenTreeViewCheckBox()
        {
            this.Font = MenConstants.AppPrimaryFont;
            base.ItemHeight = 16;
            base.ShowPlusMinus = false;
            base.ShowRootLines = false;
            base.ShowNodeToolTips = true;
            this.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
        }
        /// <summary>
        /// Customize  treeview based on design principle .so that OnDrawNode Method was override. Define Custom Design for TreeNodes and its icons and selected background Color and Collapse Icons.
        /// </summary>
        /// <param name="e">This e had contains the Current node and its Rectangle,Graphics NodeState.based on this properties we can draw our custom treeview nodes.</param>
        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = false;

            //Checks Whether appliaction was Runtime or DesignTime
            if (!DesignMode)
            {
                base.OnDrawNode(e);
                try
                {
                    //Check the node Location for repeat for binding.
                    if (e.Node.Bounds.X > 0)
                    {
                        // Assign Current Node Rectangle bounds to nodeRect
                        Rectangle nodeRect = e.Node.Bounds;

                        /*--------- 1. draw selected Node Focus Rectangle ---------*/
                        if ((e.State & TreeNodeStates.Focused) != 0 || this.SelectedNode == e.Node)
                        {
                            // create new rectangle for draw selected node background location and its size. 
                            Rectangle rect = new Rectangle(3, e.Node.Bounds.Y, this.Width, e.Node.Bounds.Height);
                            //to highlight the text when selected
                            e.Graphics.FillRectangle(_selectedBackColorBsh, rect);
                        }

                        /*--------- 2. draw expand/collapse icon ---------*/
                        Point ptExpand = new Point(nodeRect.Location.X, nodeRect.Location.Y + ((this.ItemHeight - _defaultNodeHeight) / 2)); //+ 4 +8

                        if ((e.Node.IsExpanded || e.Node.Nodes.Count < 1))
                        {
                            if (e.Node.Nodes.Count > 0)
                            {
                                //Draw Collapse Draw Icon
                                e.Graphics.DrawImage(global::Menporul.Windows.Controls.MenResource.CollapseDown, ptExpand);
                            }
                        }
                        else
                        {
                            //Draw Collapse Open Icon
                            e.Graphics.DrawImage(global::Menporul.Windows.Controls.MenResource.CollapseOpen, ptExpand);
                        }

                        /*--------- 3. draw node icon ---------*/
                        Point ptNodeIcon = new Point(nodeRect.X + 16, nodeRect.Location.Y); //+ 12

                        /*--------- 4. draw node Checkbox ---------*/
                        //Get The Default Checkbox Size
                        Size checkSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, System.Windows.Forms.VisualStyles.CheckBoxState.MixedNormal);
                        int dx = (e.Bounds.Height - checkSize.Width) / 2;

                        //checkbox Rectangle
                        //change the Y axis value for position the checkbox on center 
                        ptNodeIcon.Y = ptNodeIcon.Y + ((this.ItemHeight - this._checkBoxSize) / 2);
                        Rectangle rectCheckBox = new Rectangle(ptNodeIcon, new Size(_checkBoxSize, _checkBoxSize));

                        //Draw Checkbox Background
                        e.Graphics.FillRectangle(_bshCheckBoxBackGround, rectCheckBox);
                        //Draw Checkbox Border
                        e.Graphics.DrawRectangle(_penCheckBoxBorder, rectCheckBox);

                        /*--------- 5. draw node Checkbox checkMark ---------*/
                        if (e.Node.Checked)
                        {
                            //Draw Check mark when the item is checked
                            e.Graphics.DrawImage(_imgCheckMark, rectCheckBox);
                        }

                        /*--------- 6. draw node text ---------*/
                        nodeRect.X = nodeRect.X + 32; //+ 12 
                        nodeRect.Width += 1000;
                        nodeRect.Y += ((this.ItemHeight - this._defaultNodeHeight) / 2);
                        if (e.Node.ForeColor.Name == "0")
                        {
                            e.Node.ForeColor = MenConstants.TreeViewText;
                        }
                        e.Graphics.DrawString(e.Node.Text, this.Font, new SolidBrush(e.Node.ForeColor), nodeRect);
                    }
                }
                catch (Exception ex)
                {
                    //throw;
                }
            }
        }
        /// <summary>
        /// OnMouseClick Event was ovverirde .Get the Current selected node and assign to treeview selectedNode based on Location
        /// </summary>
        /// <param name="e">This e had contains Current node</param>
        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            if (this.SelectedNode != GetNodeAt(e.X, e.Y))
            {
                this.SelectedNode = GetNodeAt(e.X, e.Y);
            }
            if (IsCheckBox(e.X, e.Y))
            {
                e.Node.Checked = !e.Node.Checked;
            }
            else if (IsNodeStart(e.Node.Level, e.X, e.Y) && e.Button == MouseButtons.Left)
            {
                if (this.SelectedNode.IsExpanded)
                {
                    this.SelectedNode.Collapse();
                }
                else
                {
                    this.SelectedNode.Expand();
                }
            }
        }
        /// <summary>
        /// OnBeforeSelect event was ovveride .Get the Current node and clone it to assign on beforeNode.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            base.OnBeforeSelect(e);
            beforeNode = (TreeNode)e.Node.Clone();
        }
        /// <summary>
        /// OnNodeMouseDoubleClick event was ovveride .Get the Current node based on Location and set Expand/Collapse based 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
        {
            //base.OnNodeMouseDoubleClick(e);
            if (e.Button == MouseButtons.Left)
            {
                if (IsCheckBox(e.X, e.Y))
                {
                    return;
                }
                else if (!(IsNodeStart(e.Node.Level, e.X, e.Y)))
                {
                    var curNode = GetNodeAt(e.X, e.Y);
                    if (curNode.IsExpanded == beforeNode.IsExpanded)
                    {
                        if (curNode.IsExpanded)
                        {
                            curNode.Collapse();
                        }
                        else
                        {
                            curNode.Expand();
                        }
                    }
                    if (curNode.IsExpanded)
                    {
                        beforeNode.Expand();
                    }
                    else
                    {
                        beforeNode.Collapse();
                    }
                }
            }
        }
        /// <summary>
        ///  The IsNodeStart Method has used to check Whether click area has conatin the Collapse Arrow Icon.
        /// </summary>
        /// <param name="level">Current treenode Level</param>
        /// <param name="x">Current Click Area X </param>
        /// <param name="y">Current Click Area Y</param>
        /// <returns>if its Contains the Collapse Icon on current location it returns True or else return False</returns>
        private bool IsNodeStart(int level, int x, int y)
        {
            try
            {
                var node = GetNodeAt(x, y);

                int startX = node.Bounds.X - 12;
                int startY = node.Bounds.Y;

                int endX = startX + 24;
                int endY = node.Bounds.Y + node.Bounds.Height;
                if (x >= startX && x <= endX && y >= startY && y <= endY)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsCheckBox(int x, int y)
        {
            try
            {
                var node = GetNodeAt(x, y);

                int startX = node.Bounds.X + 16;
                int startY = node.Bounds.Y;

                int endX = startX + 12;
                int endY = node.Bounds.Y + node.Bounds.Height;

                if (x >= startX && x <= endX && y >= startY && y <= endY)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
        /// <summary>
        /// This is ItemHeight Proeperty is readonly only returns base Property values .it doesn't allow to set value.And Prevents the user change this property on design window
        /// </summary>
        public new int ItemHeight
        {
            get { return base.ItemHeight; }
            set { base.ItemHeight = value; }
        }
        /// <summary>
        /// This is ShowPlusMinus Proeperty is readonly only returns base Property values .it doesn't allow to set value.And Prevents the user change this property value on design window
        /// </summary>
        public new bool ShowPlusMinus
        {
            get { return base.ShowPlusMinus; }
            set { base.ShowPlusMinus = value; }
        }
        /// <summary>
        /// This is ShowRootLines Proeperty is readonly only returns base Property values .it doesn't allow to set value.And Prevents the user change this property value on design window
        /// </summary>
        public new bool ShowRootLines
        {
            get { return base.ShowRootLines; }
            set { base.ShowRootLines = value; }
        }
        /// <summary>
        /// This is ShowNodeToolTips Proeperty is readonly only returns base Property values .it doesn't allow to set value.And Prevents the user change this property value on design window
        /// </summary>
        public new bool ShowNodeToolTips
        {
            get { return base.ShowNodeToolTips; }
            set { base.ShowNodeToolTips = value; }
        }
    }
}
