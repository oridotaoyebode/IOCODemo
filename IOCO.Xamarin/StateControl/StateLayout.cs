﻿using Xamarin.Forms;

namespace IOCO.Demo.StateControl
{
    public static class StateLayout
    {
        public static readonly BindableProperty LoadingTemplateProperty = BindableProperty.CreateAttached("LoadingTemplate", typeof(StateDataTemplate), typeof(Layout<View>), default(StateDataTemplate), propertyChanged: (b, o, n) => { GetLayoutController(b).LoadingTemplate = (StateDataTemplate)n; });
        public static readonly BindableProperty SavingTemplateProperty = BindableProperty.CreateAttached("SavingTemplate", typeof(StateDataTemplate), typeof(Layout<View>), default(StateDataTemplate), propertyChanged: (b, o, n) => { GetLayoutController(b).SavingTemplate = (StateDataTemplate)n; });
        public static readonly BindableProperty EmptyTemplateProperty = BindableProperty.CreateAttached("EmptyTemplate", typeof(StateDataTemplate), typeof(Layout<View>), default(StateDataTemplate), propertyChanged: (b, o, n) => { GetLayoutController(b).EmptyTemplate = (StateDataTemplate)n; });
        public static readonly BindableProperty ErrorTemplateProperty = BindableProperty.CreateAttached("ErrorTemplate", typeof(StateDataTemplate), typeof(Layout<View>), default(StateDataTemplate), propertyChanged: (b, o, n) => { GetLayoutController(b).ErrorTemplate = (StateDataTemplate)n; });
        public static readonly BindableProperty SuccessTemplateProperty = BindableProperty.CreateAttached("SuccessTemplate", typeof(StateDataTemplate), typeof(Layout<View>), default(StateDataTemplate), propertyChanged: (b, o, n) => { GetLayoutController(b).SuccessTemplate = (StateDataTemplate)n; });
        public static readonly BindableProperty FullScreenProperty = BindableProperty.CreateAttached("FullScreen", typeof(bool), typeof(Layout<View>), false);

        public static readonly BindableProperty StateProperty = BindableProperty.CreateAttached(nameof(StateControl.State), typeof(StateControl.State), typeof(Layout<View>), StateControl.State.None, propertyChanged: (b, o, n) => OnStateChanged(b, (StateControl.State)o, (StateControl.State)n));

        static readonly BindableProperty LayoutControllerProperty = BindableProperty.CreateAttached("LayoutController", typeof(StateLayoutController), typeof(Layout<View>), default(StateLayoutController),
                 defaultValueCreator: (b) => new StateLayoutController((Layout<View>)b, GetFullScreen(b)),
                 propertyChanged: (b, o, n) => OnControllerChanged(b, (StateLayoutController)o, (StateLayoutController)n));


        public static void SetFullScreen(BindableObject b, bool value)
        {
            b.SetValue(FullScreenProperty, value);
        }

        public static bool GetFullScreen(BindableObject b)
        {
            return (bool)b.GetValue(FullScreenProperty);
        }
        public static void SetState(BindableObject b, StateControl.State value)
        {
            b.SetValue(StateProperty, value);
        }

        public static StateControl.State GetState(BindableObject b)
        {
            return (StateControl.State)b.GetValue(StateProperty);
        }

        public static void SetLoadingTemplate(BindableObject b, DataTemplate value)
        {
            b.SetValue(LoadingTemplateProperty, value);
        }

        public static StateDataTemplate GetLoadingTemplate(BindableObject b)
        {
            return (StateDataTemplate)b.GetValue(LoadingTemplateProperty);
        }

        public static void SetSavingTemplate(BindableObject b, DataTemplate value)
        {
            b.SetValue(SavingTemplateProperty, value);
        }

        public static StateDataTemplate GetSavingTemplate(BindableObject b)
        {
            return (StateDataTemplate)b.GetValue(SavingTemplateProperty);
        }

        public static void SetEmptyTemplate(BindableObject b, DataTemplate value)
        {
            b.SetValue(EmptyTemplateProperty, value);
        }

        public static StateDataTemplate GetEmptyTemplate(BindableObject b)
        {
            return (StateDataTemplate)b.GetValue(EmptyTemplateProperty);
        }

        public static void SetErrorTemplate(BindableObject b, DataTemplate value)
        {
            b.SetValue(ErrorTemplateProperty, value);
        }

        public static StateDataTemplate GetErrorTemplate(BindableObject b)
        {
            return (StateDataTemplate)b.GetValue(ErrorTemplateProperty);
        }

        public static void SetSuccessTemplate(BindableObject b, DataTemplate value)
        {
            b.SetValue(SuccessTemplateProperty, value);
        }

        public static StateDataTemplate GetSuccessTemplate(BindableObject b)
        {
            return (StateDataTemplate)b.GetValue(SuccessTemplateProperty);
        }

        static void OnStateChanged(BindableObject bindable, StateControl.State oldValue, StateControl.State newValue)
        {
            // Swap out the current children for the Loading Template.
            if (oldValue != newValue && newValue != StateControl.State.None)
            {
                GetLayoutController(bindable).SwitchToTemplate(newValue);
            }
            else if (oldValue != newValue && newValue == StateControl.State.None)
            {
                GetLayoutController(bindable).SwitchToContent();
            }
        }

        static StateLayoutController GetLayoutController(BindableObject b)
        {
            return (StateLayoutController)b.GetValue(LayoutControllerProperty);
        }

        static void SetLayoutController(BindableObject b, StateLayoutController value)
        {
            b.SetValue(LayoutControllerProperty, value);
        }

        static void OnControllerChanged(BindableObject b, StateLayoutController oldC, StateLayoutController newC)
        {
            if (newC == null)
            {
                return;
            }

            newC.LoadingTemplate = GetLoadingTemplate(b);
            newC.SavingTemplate = GetSavingTemplate(b);
            newC.EmptyTemplate = GetEmptyTemplate(b);
            newC.ErrorTemplate = GetErrorTemplate(b);
            newC.SuccessTemplate = GetSuccessTemplate(b);
        }
    }
}
