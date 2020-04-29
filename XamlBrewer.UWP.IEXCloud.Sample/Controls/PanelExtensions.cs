using System;
using Windows.Foundation.Metadata;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace XamlBrewer.Fluent
{
    public static partial class PanelExtensions
    {
        public static void RegisterImplicitAnimations(this Panel panel)
        {
            // Check if SDK > 14393
            if (!ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 3))
            {
                return;
            }

            var compositor = ElementCompositionPreview.GetElementVisual(panel).Compositor;

            // Create ImplicitAnimations Collection. 
            var elementImplicitAnimation = compositor.CreateImplicitAnimationCollection();

            // Define trigger and animation that should play when the trigger is triggered. 
            elementImplicitAnimation["Offset"] = CreateOffsetAnimation(compositor);

            foreach (var item in panel.Children)
            {
                var elementVisual = ElementCompositionPreview.GetElementVisual(item);
                elementVisual.ImplicitAnimations = elementImplicitAnimation;
            }
        }

        private static CompositionAnimationGroup CreateOffsetAnimation(Compositor compositor)
        {
            // Define Offset Animation for the Animation group
            Vector3KeyFrameAnimation offsetAnimation = compositor.CreateVector3KeyFrameAnimation();
            offsetAnimation.InsertExpressionKeyFrame(1.0f, "this.FinalValue");
            offsetAnimation.Duration = TimeSpan.FromSeconds(.3);

            // Define Animation Target for this animation to animate using definition. 
            offsetAnimation.Target = "Offset";

            // Add Animation to Animation group. 
            CompositionAnimationGroup animationGroup = compositor.CreateAnimationGroup();
            animationGroup.Add(offsetAnimation);

            return animationGroup;
        }
    }
}
