﻿using System;
using Gwen.Control;

namespace Gwen.ControlInternal
{
    /// <summary>
    /// Tree node label.
    /// </summary>
    public class TreeNodeLabel : Button
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNodeLabel"/> class.
        /// </summary>
        /// <param name="parent">Parent control.</param>
        public TreeNodeLabel(Base parent)
            : base(parent)
        {
            Alignment = Pos.Left | Pos.CenterV;
            ShouldDrawBackground = false;
            Height = 16;
        }

        /// <summary>
        /// Updates control colors.
        /// </summary>
        public override void UpdateColors()
        {
            if (IsDisabled)
            {
                TextColor = Skin.Colors.Button.Disabled;
                return;
            }

            if (IsDepressed || ToggleState)
            {
                TextColor = Skin.Colors.Tree.Selected;
                return;
            }

            if (IsHovered)
            {
                TextColor = Skin.Colors.Tree.Hover;
                return;
            }

            TextColor = Skin.Colors.Tree.Normal;
        }
    }
}
