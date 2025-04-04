
# Changelog

## [1.0.1] - 2025-04-03

### Added

- Add set without notify functions ([`a0fbd98`](https://github.com/JanSharp/VRCGenericValueEditor/commit/a0fbd98212412e410fa1cf9db03abbdcd26e23e9))

### Fixed

- Add workaround for input fields not showing VRChat keyboard and popup ([`8e93ba2`](https://github.com/JanSharp/VRCGenericValueEditor/commit/8e93ba29ad1b4ce399afb59a8ace85ab11f00c3f))
- Set WidgetManager prefab transform position to 0 0 0 ([`5b6e679`](https://github.com/JanSharp/VRCGenericValueEditor/commit/5b6e679462cc220b83af45caf0ba57e0571c39e9))

## [1.0.0] - 2025-03-22

### Added

- Add readme with documentation ([`faf7ba6`](https://github.com/JanSharp/VRCGenericValueEditor/commit/faf7ba66b52ace1cccf60138f81d374119b93030), [`a08997b`](https://github.com/JanSharp/VRCGenericValueEditor/commit/a08997ba3a28e1ee064c38583491e46c550ac572), [`f9e4dd1`](https://github.com/JanSharp/VRCGenericValueEditor/commit/f9e4dd13147dc12368f51e2fd022094c046c7843))
- Add docs through xml annotations ([`473a830`](https://github.com/JanSharp/VRCGenericValueEditor/commit/473a8302a20f23fe54c9a11b9c31d146c36b5f63))
- Add StdMoveWidget to reduce required casts ([`c50abf9`](https://github.com/JanSharp/VRCGenericValueEditor/commit/c50abf9d4a939d98766326410057bef6e9591517))
- Add api function to register custom widgets ([`329c227`](https://github.com/JanSharp/VRCGenericValueEditor/commit/329c227f37d2fd1bd103ba43cb0506d34cf3bda1))
- Add WidgetManager with shared widget pooling ([`d2815e7`](https://github.com/JanSharp/VRCGenericValueEditor/commit/d2815e795a3931d628042c9a89a18c69e7dbdb9f), [`3184bd3`](https://github.com/JanSharp/VRCGenericValueEditor/commit/3184bd3c3cc0202a6d7268f190ac9ea21a98feae), [`d1eeaa1`](https://github.com/JanSharp/VRCGenericValueEditor/commit/d1eeaa12b8fc2d47490d7625c35d64abd277cf58), [`7372130`](https://github.com/JanSharp/VRCGenericValueEditor/commit/73721306f94045fccdd903d67ef775cab6d3de63), [`4b8f03f`](https://github.com/JanSharp/VRCGenericValueEditor/commit/4b8f03f977a45280b8fafffa750315139e368063), [`1d4259d`](https://github.com/JanSharp/VRCGenericValueEditor/commit/1d4259dc8e08eafdfe77fd941f2a16b883c9c246))
- Add GenericValueEditor pane with widget drawing ([`3190fe3`](https://github.com/JanSharp/VRCGenericValueEditor/commit/3190fe35ada4c5a700016ce2db9fc949db0d8fb7), [`0f326ec`](https://github.com/JanSharp/VRCGenericValueEditor/commit/0f326ec3aea073d3ea5932df3278186594a2641b), [`2a53d67`](https://github.com/JanSharp/VRCGenericValueEditor/commit/2a53d675ad360f6ff34a17d465683b6180ebcb56), [`515f54f`](https://github.com/JanSharp/VRCGenericValueEditor/commit/515f54fe1c73a5c48dd6ee69c29da376fd7d32f8))
- Add concept of container widgets (Box, FoldOut, Grouping, Indent) with children ([`5c66a17`](https://github.com/JanSharp/VRCGenericValueEditor/commit/5c66a1756835199b9425318e49fd0f40f72339ed), [`b19a929`](https://github.com/JanSharp/VRCGenericValueEditor/commit/b19a929334a33e547f3a87e7fdf6c4e32d9c5d20), [`95db413`](https://github.com/JanSharp/VRCGenericValueEditor/commit/95db4132173792bcd50d18bebb8b77dda1af22ed))
- Add Interactable option to all widgets ([`4cc11a0`](https://github.com/JanSharp/VRCGenericValueEditor/commit/4cc11a0b47f56d9a5c7c05f88f346ae8b7e1d37f))
- Add IsVisible property to all widgets ([`76c77bf`](https://github.com/JanSharp/VRCGenericValueEditor/commit/76c77bf306d29ccbe3369d34e76e8cff1a0a2cbc))
- Add Grouping widget ([`3cbf028`](https://github.com/JanSharp/VRCGenericValueEditor/commit/3cbf02838f0e101a0f414c9c2a198dc18866cda7))
- Add FoldOut widget ([`2ce5047`](https://github.com/JanSharp/VRCGenericValueEditor/commit/2ce50471452ef6a69167e4f0bd5ecd23f82e7f30))
- Add Space widget ([`b8500f2`](https://github.com/JanSharp/VRCGenericValueEditor/commit/b8500f2552acad37eb77ded97530afc97b7e5ba2))
- Add Vector3Field widget ([`55ef1f3`](https://github.com/JanSharp/VRCGenericValueEditor/commit/55ef1f3134ef2648ce66d0de6923da5d50cdb139), [`b4635ee`](https://github.com/JanSharp/VRCGenericValueEditor/commit/b4635ee8d4b38359fea9dd6df4d04b80d26437e8), [`1717afa`](https://github.com/JanSharp/VRCGenericValueEditor/commit/1717afa2eb49b373f34591c7dc3c4790c881d580), [`a4d52e3`](https://github.com/JanSharp/VRCGenericValueEditor/commit/a4d52e304e8b0c500c12621a964dd6f0a1c9afe8))
- Add Vector2Field widget ([`a22e650`](https://github.com/JanSharp/VRCGenericValueEditor/commit/a22e6506158ab78964530857ee5c63dbe0ed5063), [`b4635ee`](https://github.com/JanSharp/VRCGenericValueEditor/commit/b4635ee8d4b38359fea9dd6df4d04b80d26437e8), [`1717afa`](https://github.com/JanSharp/VRCGenericValueEditor/commit/1717afa2eb49b373f34591c7dc3c4790c881d580), [`a4d52e3`](https://github.com/JanSharp/VRCGenericValueEditor/commit/a4d52e304e8b0c500c12621a964dd6f0a1c9afe8))
- Add DecimalField widget supporting `float`, `double` and `decimal` ([`9a16980`](https://github.com/JanSharp/VRCGenericValueEditor/commit/9a169802c45e8a601cb1f6b73338524605652de4), [`b4635ee`](https://github.com/JanSharp/VRCGenericValueEditor/commit/b4635ee8d4b38359fea9dd6df4d04b80d26437e8), [`31dabec`](https://github.com/JanSharp/VRCGenericValueEditor/commit/31dabec3aa914995e103b738b06ef7c0a8d17e5e), [`a4d52e3`](https://github.com/JanSharp/VRCGenericValueEditor/commit/a4d52e304e8b0c500c12621a964dd6f0a1c9afe8))
- Add IntegerField widget supporting `int`, `uint`, `long` and `ulong` ([`c7e3725`](https://github.com/JanSharp/VRCGenericValueEditor/commit/c7e37258d6a69bdcf6dfa1cbac7a88899543606d), [`b4635ee`](https://github.com/JanSharp/VRCGenericValueEditor/commit/b4635ee8d4b38359fea9dd6df4d04b80d26437e8), [`31dabec`](https://github.com/JanSharp/VRCGenericValueEditor/commit/31dabec3aa914995e103b738b06ef7c0a8d17e5e), [`a4d52e3`](https://github.com/JanSharp/VRCGenericValueEditor/commit/a4d52e304e8b0c500c12621a964dd6f0a1c9afe8))
- Add MultilineStringField widget ([`ff02290`](https://github.com/JanSharp/VRCGenericValueEditor/commit/ff0229043945a0d87fd01eeef77be6df2b751f56), [`567b603`](https://github.com/JanSharp/VRCGenericValueEditor/commit/567b60341f86fbc11c1dfa99d40ee70a9ee436ee), [`b4635ee`](https://github.com/JanSharp/VRCGenericValueEditor/commit/b4635ee8d4b38359fea9dd6df4d04b80d26437e8))
- Add StringField widget ([`ff02290`](https://github.com/JanSharp/VRCGenericValueEditor/commit/ff0229043945a0d87fd01eeef77be6df2b751f56), [`567b603`](https://github.com/JanSharp/VRCGenericValueEditor/commit/567b60341f86fbc11c1dfa99d40ee70a9ee436ee), [`b4635ee`](https://github.com/JanSharp/VRCGenericValueEditor/commit/b4635ee8d4b38359fea9dd6df4d04b80d26437e8))
- Add Line widget ([`e765c74`](https://github.com/JanSharp/VRCGenericValueEditor/commit/e765c740277c63746245b2ffecaf55c1534a3455))
- Add Indent widget ([`b72421b`](https://github.com/JanSharp/VRCGenericValueEditor/commit/b72421b20571400717ac67f0fe5c2f86ce63f07c))
- Add Box widget ([`0f326ec`](https://github.com/JanSharp/VRCGenericValueEditor/commit/0f326ec3aea073d3ea5932df3278186594a2641b))
- Add Label widget ([`3f6bd2d`](https://github.com/JanSharp/VRCGenericValueEditor/commit/3f6bd2d9feb10c890934ef56146cf331d7f769fb))
- Add Button widget ([`3190fe3`](https://github.com/JanSharp/VRCGenericValueEditor/commit/3190fe35ada4c5a700016ce2db9fc949db0d8fb7))
- Add SliderField widget ([`3190fe3`](https://github.com/JanSharp/VRCGenericValueEditor/commit/3190fe35ada4c5a700016ce2db9fc949db0d8fb7), [`e7163fb`](https://github.com/JanSharp/VRCGenericValueEditor/commit/e7163fbbd94d493816246b730c99e7750ca2cd1c), [`8322070`](https://github.com/JanSharp/VRCGenericValueEditor/commit/8322070fbcea0d4c07c1213004e3b4d960cc9494), [`87939e8`](https://github.com/JanSharp/VRCGenericValueEditor/commit/87939e8d67973f2eccfbfee475e601d8cbe050e2), [`6ffa170`](https://github.com/JanSharp/VRCGenericValueEditor/commit/6ffa1703ab079fe349efab7692168d3a09daf657), [`b4635ee`](https://github.com/JanSharp/VRCGenericValueEditor/commit/b4635ee8d4b38359fea9dd6df4d04b80d26437e8), [`a7a080f`](https://github.com/JanSharp/VRCGenericValueEditor/commit/a7a080f16c274f6239ab13ac465cb7acb66f8500), [`a4d52e3`](https://github.com/JanSharp/VRCGenericValueEditor/commit/a4d52e304e8b0c500c12621a964dd6f0a1c9afe8))
- Add ToggleField widget ([`3190fe3`](https://github.com/JanSharp/VRCGenericValueEditor/commit/3190fe35ada4c5a700016ce2db9fc949db0d8fb7), [`e7163fb`](https://github.com/JanSharp/VRCGenericValueEditor/commit/e7163fbbd94d493816246b730c99e7750ca2cd1c))

[1.0.1]: https://github.com/JanSharp/VRCGenericValueEditor/releases/tag/v1.0.1
[1.0.0]: https://github.com/JanSharp/VRCGenericValueEditor/releases/tag/v1.0.0
