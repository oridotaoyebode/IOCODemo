using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace IOCO.Demo.StateControl
{
    public class StateLayoutController
    {
        private readonly WeakReference<Layout<View>> _layoutWeakReference;
        private bool _layoutIsGrid = false;
        private bool _fullScreen = false;
        private IList<View> _originalContent;
        private StateControl.State _previousState = StateControl.State.None;

        public StateDataTemplate LoadingTemplate { get; set; }
        public StateDataTemplate SavingTemplate { get; set; }
        public StateDataTemplate EmptyTemplate { get; set; }
        public StateDataTemplate ErrorTemplate { get; set; }
        public StateDataTemplate SuccessTemplate { get; set; }

        public StateLayoutController(Layout<View> layout, bool fullScreen = false)
        {
            _layoutWeakReference = new WeakReference<Layout<View>>(layout);
            _fullScreen = fullScreen;
        }

        public void SwitchToContent()
        {
            Layout<View> layout;

            if (!_layoutWeakReference.TryGetTarget(out layout))
            {
                return;
            }

            _previousState = StateControl.State.None;

            // Put the original content back in.
            layout.Children.Clear();

            foreach (var item in _originalContent)
            {
                layout.Children.Add(item);
            }
        }

        public void SwitchToTemplate(StateControl.State state)
        {
            Layout<View> layout;

            if (!_layoutWeakReference.TryGetTarget(out layout))
            {
                return;
            }

            // Put the original content somewhere where we can restore it.
            if (_previousState == StateControl.State.None)
            {
                _originalContent = new List<View>();

                foreach (var item in layout.Children)
                    _originalContent.Add(item);
            }

            if (HasTemplateForState(state))
            {
                _previousState = state;

                // Add the loading template.
                layout.Children.Clear();

                var repeatCount = GetRepeatCount(state);

                if (layout is Grid)
                {
                    _layoutIsGrid = true;
                    if (_fullScreen && GetRepeatCount(state) <= 1)
                    {
                        //Do nothing. 
                    }
                    else
                    {
                        layout.Children.Add(new StackLayout());
                        
                    }
                    
                    
                }
                for (int i = 0; i < repeatCount; i++)
                {
                    if (_layoutIsGrid && !_fullScreen && GetRepeatCount(state) > 1)
                    {
                        if (layout.Children[0] is StackLayout stack)
                        {
                            var view = CreateItemView(layout, state);

                            if (view != null)
                            {
                                stack.Children.Add(view);
                            }
                        }
                    }
                    else
                    {
                        var view = CreateItemView(layout, state);
                        if (_fullScreen && _layoutIsGrid)
                        {
                            var rowCount = ((Grid) layout).RowDefinitions.Count;
                            var columnCount = ((Grid) layout).ColumnDefinitions.Count;
                            view.SetValue(Grid.RowSpanProperty, rowCount);
                            view.SetValue(Grid.ColumnDefinitionsProperty, columnCount);
                        }
                        if (view != null)
                        {
                            layout.Children.Add(view);
                        }
                    }
                }
            }
        }

        private bool HasTemplateForState(StateControl.State state)
        {
            switch (state)
            {
                case StateControl.State.Loading:
                    return LoadingTemplate != null;
                case StateControl.State.Saving:
                    return SavingTemplate != null;
                case StateControl.State.Success:
                    return SuccessTemplate != null;
                case StateControl.State.Error:
                    return ErrorTemplate != null;
                case StateControl.State.Empty:
                    return EmptyTemplate != null;
            }

            return false;
        }

        private int GetRepeatCount(StateControl.State state)
        {
            switch (state)
            {
                case StateControl.State.Loading:
                    return LoadingTemplate != null ? LoadingTemplate.RepeatCount : 1;
                case StateControl.State.Saving:
                    return SavingTemplate != null ? SavingTemplate.RepeatCount : 1;
                case StateControl.State.Success:
                    return SuccessTemplate != null ? SuccessTemplate.RepeatCount : 1;
                case StateControl.State.Error:
                    return ErrorTemplate != null ? ErrorTemplate.RepeatCount : 1;
                case StateControl.State.Empty:
                    return EmptyTemplate != null ? EmptyTemplate.RepeatCount : 1;
                case StateControl.State.None:
                    break;
            }

            return 1;
        }

        /// <summary>
        /// Expand the LoadingDataTemplate or use the template selector.
        /// </summary>
        /// <returns>The item view.</returns>
        View CreateItemView(Layout<View> layout, StateControl.State state)
        {
            switch (state)
            {
                case StateControl.State.Loading:
                    return CreateItemView(LoadingTemplate, state);
                case StateControl.State.Saving:
                    return CreateItemView(SavingTemplate, state);
                case StateControl.State.Success:
                    return CreateItemView(SuccessTemplate, state);
                case StateControl.State.Error:
                    return CreateItemView(ErrorTemplate, state);
                case StateControl.State.Empty:
                    return CreateItemView(EmptyTemplate, state);
                case StateControl.State.None:
                    break;
            }

            return null;
        }

        /// <summary>
        /// Expand the Loading Data Template.
        /// </summary>
        /// <returns>The item view.</returns>
        /// <param name="dataTemplate">Data template.</param>
        View CreateItemView(StateDataTemplate dataTemplate, StateControl.State state)
        {
            if (dataTemplate != null)
            {
                var view = (View)dataTemplate.Template.CreateContent();
                return view;
            }
            else
            {
                return new Label() { Text = $"[{state.ToString()}Template] not defined." };
            }
        }
    }
}
