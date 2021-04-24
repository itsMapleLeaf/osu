﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.Mania.Configuration;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Mania.Tests
{
    public abstract class ManiaInputTestScene : OsuTestScene
    {
        private readonly Container<Drawable> content;
        protected override Container<Drawable> Content => content ?? base.Content;

        [Cached]
        protected readonly Bindable<bool> ConfigColourCodedNotes = new Bindable<bool>();

        protected ManiaInputTestScene(int keys)
        {
            base.Content.Add(content = new LocalInputManager(keys));
        }

        [BackgroundDependencyLoader]
        private void load(RulesetConfigCache configCache)
        {
            var config = (ManiaRulesetConfigManager)configCache.GetConfigFor(Ruleset.Value.CreateInstance());
            config.BindWith(ManiaRulesetSetting.ColourCodedNotes, ConfigColourCodedNotes);
        }

        private class LocalInputManager : ManiaInputManager
        {
            public LocalInputManager(int variant)
                : base(new ManiaRuleset().RulesetInfo, variant)
            {
            }

            protected override KeyBindingContainer<ManiaAction> CreateKeyBindingContainer(RulesetInfo ruleset, int variant, SimultaneousBindingMode unique)
                => new LocalKeyBindingContainer(ruleset, variant, unique);

            private class LocalKeyBindingContainer : RulesetKeyBindingContainer
            {
                public LocalKeyBindingContainer(RulesetInfo ruleset, int variant, SimultaneousBindingMode unique)
                    : base(ruleset, variant, unique)
                {
                }

                protected override void ReloadMappings()
                {
                    KeyBindings = DefaultKeyBindings;
                }
            }
        }
    }
}
