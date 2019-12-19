﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace ItemsRepeaterTestApp.Samples.Selection
{
    public class GroupedRepeaterItemAutomationPeer : FrameworkElementAutomationPeer, ISelectionItemProvider
    {
        private GroupedRepeaterItem _owner;
        public GroupedRepeaterItemAutomationPeer(FrameworkElement owner) : base(owner)
        {
            _owner = (GroupedRepeaterItem)owner;
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            if (patternInterface == PatternInterface.SelectionItem)
            {
                return this;
            }

            return base.GetPattern(patternInterface);
        }

        public void AddToSelection()
        {
            _owner.Select();
        }

        public void RemoveFromSelection()
        {
            _owner.Deselect();
        }

        public void Select()
        {
            _owner.Select();
        }

        public bool IsSelected
        {
            get
            {
                return _owner.IsSelected;
            }
        }

        public IRawElementProviderSimple SelectionContainer
        {
            get
            {
                // TODO: could cache this to avoid the walk every time
                IRawElementProviderSimple containerPeer = null;
                FrameworkElement element = _owner;
                while (element != null && !(element is SelectionContainer))
                {
                    element = (FrameworkElement)element.Parent;
                }

                if (element != null)
                {
                    var container = (SelectionContainer)element;
                    containerPeer = ProviderFromPeer(CreatePeerForElement(container));
                }

                return containerPeer;
            }
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.ListItem;
        }

        protected override string GetLocalizedControlTypeCore()
        {
            return "Item";
        }
    }
}