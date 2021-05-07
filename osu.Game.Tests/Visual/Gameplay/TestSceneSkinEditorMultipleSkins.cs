// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Testing;
using osu.Game.Rulesets;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Osu;
using osu.Game.Rulesets.Scoring;
using osu.Game.Screens.Play;
using osu.Game.Skinning.Editor;
using osuTK.Input;

namespace osu.Game.Tests.Visual.Gameplay
{
    public class TestSceneSkinEditorMultipleSkins : SkinnableTestScene
    {
        [SetUpSteps]
        public void SetUpSteps()
        {
            AddStep("create editor overlay", () =>
            {
                SetContents(() =>
                {
                    var ruleset = new OsuRuleset();
                    var working = CreateWorkingBeatmap(ruleset.RulesetInfo);
                    var beatmap = working.GetPlayableBeatmap(ruleset.RulesetInfo);

                    ScoreProcessor scoreProcessor = new ScoreProcessor();

                    var drawableRuleset = ruleset.CreateDrawableRulesetWith(beatmap);

                    var hudOverlay = new HUDOverlay(scoreProcessor, drawableRuleset, Array.Empty<Mod>())
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    };

                    // Add any key just to display the key counter visually.
                    hudOverlay.KeyCounter.Add(new KeyCounterKeyboard(Key.Space));

                    return new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Children = new Drawable[]
                        {
                            drawableRuleset,
                            hudOverlay,
                            new SkinEditor(hudOverlay),
                        }
                    };
                });
            });
        }

        protected override Ruleset CreateRulesetForSkinProvider() => new OsuRuleset();
    }
}
