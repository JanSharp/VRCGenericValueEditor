
Think about IMGUI. Then think about Udon. Become sad.

- [ ] potentially a drop down widget
- [x] visible option for all widgets, to allow toggling without having to redraw
- [x] interactable option for all widgets
  - [ ] non interactable widgets should be darkened, not red (Would require manually changing the color of boxes and text and I'm not interested right now)
- [x] step value for sliders, which also enables int sliders
- [x] range limitations on integer and decimal field widgets
- [x] custom widgets
- [x] manager which also keeps widget instances pooled for 5 minutes
- [x] write some basic docs
- [x] probably make input fields respond to text change 1 frame delayed. Yes I'm very salty about the on end edit bug not getting fixed
- [x] ~~add format to vector field ToString calls~~ nope, just let people type in whatever they want without formatting it
- [ ] the ability to have multiple buttons next to each other would be great. Though horizontal flows seem problematic for most other widgets
- [ ] toggle groups would be great
- [x] set value without notify on everything that has a value and raises events on value change
- [x] ~~input fields do not open the VRChat input field~~ Go up vote https://feedback.vrchat.com/bug-reports/p/instantiated-input-fields-dont-open-the-vrc-keyboard
  - [x] try having the "prefab" referenced by the manager actually be an instance of the prefab in the scene
