using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Menporul.Windows.Controls.Core;
using System.ComponentModel;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// MenTreeView is Custom Control for Treeview .It has Override the default treeview and its make its has rich treeview based on the Design Principle. 
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public partial class MenTreeView : TreeView
    {
        /// <summary>
        /// This node is used to store before selecte node on the Event of BeforeSelect.
        /// Based on this node Expand and Collapse Will occur. 
        /// </summary>        
        private TreeNode _beforeNode;

        /// <summary>
        /// This brush is used to set the Background Color on Selected Node.
        /// </summary>
        private Brush _selectedBackColorBsh = new SolidBrush(MenConstants.TreeViewSelectedBackGround);

        /// <summary>
        /// This brush is used to set the Dark Background Color on Selected Node.
        /// </summary>
        private Brush _selectedDarkBackColorBsh = new SolidBrush(MenConstants.TreeViewSelectedDarkBackGround);

        /// <summary>
        /// This brush is used to set the Tree node Text ForeGround Color.
        /// </summary>
        private Brush _textBsh = new SolidBrush(MenConstants.TreeViewText);

        /// <summary>
        /// This brush is used to set the selected Tree node Text ForeGround Color.
        /// </summary>
        private Brush _selectedTextBsh = new SolidBrush(MenConstants.TreeViewSelectedText);
        /// <summary>
        /// Set the Default Height of the Treeview Nodes
        /// </summary>
        private int _defaultNodeHeight = 16;
        /// <summary>
        /// Constructor initialize and set the Particular Property values for Custom Design 
        /// </summary>
        /// <param name="Font">Set the Custom Design Font For TreeView</param>
        /// <param name="ItemHeight">set the Custom ItemHeight for Treeview.It's represents the space between two nodes.</param>
        /// <param name="ShowPlusMinus">set False for this property for remove the Plus/Minus Icon on treenodes </param>
        /// <param name="ShowRootLines">set false for this property for remove the default lines on treeview nodes</param>
        /// <param name="ShowNodeToolTips">set treu for this property for view the ToolTip on treenode MouseOver</param>
        /// <param name="DrawMode">Set OwnerDrawnAll for Draw Custom TreeView based on our Design Standards</param>
        public MenTreeView()
        {
            this.Font = MenConstants.AppPrimaryFont;
            // set Node Height . it defines the space between other nodes
            base.ItemHeight = _defaultNodeHeight;
            //hide the Plus Minus Icon in the Treeview
            base.ShowPlusMinus = false;
            //Hide the Root lines in the Treeview
            base.ShowRootLines = false;
            //Enable ToolTips Showing
            base.ShowNodeToolTips = true;
            //Enable User Drawn  Options
            this.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
        }
        /// <summary>
        /// Customize  treeview based on design principle .so that OnDrawNode Method was override. Define Custom Design for TreeNodes and its icons and selected background Color and Collapse Icons.
        /// </summary>
        /// <param name="e">This e had contains the Current node and its Rectangle,Graphics NodeState.based on this properties we can draw our custom treeview nodes.</param>
        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            //Checks Whether appliaction was Runtime or DesignTime
            if (!DesignMode)
            {
                //base.OnDrawNode(e);
                try
                {
                    //Check the node Location for repeat for binding.
                    if (e.Node.Bounds.X != 0 && this.ImageList != null && this.ImageList.Images.Count > 0)
                    {
                        // Assign Current Node Rectangle bounds to nodeRect
                        Rectangle nodeRect = e.Node.Bounds;
                        bool IsFocus = false;

                        /*--------- 1. draw selected Node Focus Rectangle ---------*/
                        if ((e.State & TreeNodeStates.Focused) != 0 || this.SelectedNode == e.Node)
                        {
                            // create new rectangle for draw selected node background location and its size. 
                            Rectangle rect = new Rectangle(3, e.Node.Bounds.Y, this.Width, e.Node.Bounds.Height);
                            //to highlight the text when selected
                            e.Graphics.FillRectangle(EnableHighLightDark ? _selectedDarkBackColorBsh : _selectedBackColorBsh, rect);
                            IsFocus = true;
                        }

                        /*--------- 2. draw expand/collapse icon ---------*/
                        Point ptExpand = new Point(nodeRect.Location.X - 8, nodeRect.Location.Y + ((this.ItemHeight - _defaultNodeHeight) / 2)); //+ 4 +8
                        Image expandImg = null;
                        if ((e.Node.IsExpanded || e.Node.Nodes.Count < 1))
                        {
                            if (e.Node.Nodes.Count > 0)
                            {
                                expandImg = global::Menporul.Windows.Controls.MenResource.CollapseDown;
                                //Draw Collapse Down Draw Icon
                                e.Graphics.DrawImage(expandImg, ptExpand);
                            }
                        }
                        else
                        {
                            expandImg = global::Menporul.Windows.Controls.MenResource.CollapseOpen;
                            //Draw Collapse Open Icon
                            e.Graphics.DrawImage(expandImg, ptExpand);
                        }

                        /*--------- 3. draw node icon ---------*/
                        Point ptNodeIcon = new Point(nodeRect.Location.X + 12, nodeRect.Location.Y); //+ 12
                        Image nodeImg;
                        if (this.ImageList != null)
                            nodeImg = this.ImageList.Images[e.Node.ImageIndex];
                        else
                            nodeImg = this.ImageList.Images[0];
                        e.Graphics.DrawImage(nodeImg, ptNodeIcon);


                        /*--------- 4. draw node text ---------*/
                        nodeRect.X = nodeRect.X + 32; //+ 12 
                        nodeRect.Width += 1000;
                        nodeRect.Y += ((this.ItemHeight - _defaultNodeHeight) / 2);
                        Font nodeFont = e.Node.NodeFont;
                        if (nodeFont == null)
                            nodeFont = this.Font;
                        //Draw Node Custom Backcolor except Window or Highlight
                        if (e.Node.BackColor.Name.ToString() != "0" && e.Node.BackColor.Name.ToString() != "Window" && e.Node.BackColor.Name.ToString() != "Highlight")
                        {
                            SizeF sz = e.Graphics.MeasureString(e.Node.Text, nodeFont);
                            e.Graphics.FillRectangle(new SolidBrush(e.Node.BackColor), new Rectangle(nodeRect.X, nodeRect.Y, Convert.ToInt32(sz.Width), Convert.ToInt32(sz.Height)));
                        }
                        if (IsFocus && EnableHighLightDark)
                        {
                            e.Graphics.DrawString(e.Node.Text, nodeFont, _selectedTextBsh, nodeRect);
                        }
                        else
                        {
                            e.Graphics.DrawString(e.Node.Text, nodeFont, _textBsh, nodeRect);
                        }
                    }
                }
                catch (Exception ex)
                {
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
            if (IsNodeStart(e.Node.Level, e.X, e.Y) && e.Button == MouseButtons.Left)
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
            _beforeNode = (TreeNode)e.Node.Clone();

        }
        /// <summary>
        /// OnNodeMouseDoubleClick event was ovveride .Get the Current node based on Location and set Expand/Collapse based 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseDoubleClick(e);
            if (!(IsNodeStart(e.Node.Level, e.X, e.Y) && e.Button == MouseButtons.Left))
            {
                var curNode = GetNodeAt(e.X, e.Y);
                if (curNode.IsExpanded == _beforeNode.IsExpanded)
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
                    _beforeNode.Expand();
                }
                else
                {
                    _beforeNode.Collapse();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);
            DoDragDrop(e.Item, DragDropEffects.Move);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            e.Effect = DragDropEffects.Move;
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
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// its enable /Disable the Dark Hightlight on treeview
        /// </summary>
        [Browsable(true)]
        [DefaultValue(false)]
        public bool EnableHighLightDark { get; set; }
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
