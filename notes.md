
Think about IMGUI. Then think about Udon. Become sad.

- [ ] potentially a drop down widget
- [x] visible option for all widgets, to allow toggling without having to redraw
- [x] interactable option for all widgets
  - [ ] non interactable widgets should be darkened, not red (Would require manually changing the color of boxes and text and I'm not interested right now)
- [x] step value for sliders, which also enables int sliders
- [x] range limitations on integer and decimal field widgets
- [x] custom widgets
- [x] manager which also keeps widget instances pooled for 5 minutes
- [ ] write some basic docs
- [ ] probably make input fields respond to text change 1 frame delayed. Yes I'm very salty about the on end edit bug not getting fixed
