using System.Collections.Generic;

namespace Randomizer
{
    public class DeathLinkMessages
    {
        public static Dictionary<string, List<string>> Causes = new Dictionary<
            string,
            List<string>
        >()
        {
            {
                "Generic",
                new List<string>()
                {
                    "'s prayer went unheard.",
                    " didn't share their wisdom.",
                    "'s HP fell to 0.",
                    " sought their own ruin.",
                    " didn't follow the Golden Path.",
                }
            },
            {
                "Fortress Arena",
                new List<string>() { " was Sieged by the Engine." }
            },
            {
                "Fortress Basement",
                new List<string>() { " got lost in the dark." }
            },
            {
                "Library Exterior",
                new List<string>() { " didn't return their libray books." }
            },
        };

        public static Dictionary<string, string> HitTriggerCauses = new Dictionary<string, string>()
        {
            { "voidtouched", " was touched by the void." },
            { "woodcutter", " was turned to lumber." },
            { "centipede", " was too afraid of centipedes." },
            { "administrator", " tried praying to an administrator." },
            { "crow_voidtouched", " was hushed." },
            { "bomezome_easy", " could not flee the fleemers." },
        };

        public static Dictionary<string, string> HitTriggerDescriptions = new Dictionary<
            string,
            string
        >()
        {
            { "shotgun", " a shotgun." },
            { "ghostknight", " a ghost knight." },
            { "phage_spin", " a slorm...?" },
            { "phage", " a slorm...?" },
            { "zombie", " a past ruin seeker." },
            { "zombieFast", " a seeker of ruin." },
            { "voidtouched", " a voidtouched." },
            { "woodcutter", " a woodcutter." },
            { "scavengerBoss_kick", " a kick from the Boss Scavenger." },
        };

        public static List<string> GenericMessages = new List<string>()
        {
            " was killed by",
            " was defeated by",
            " died to",
            " was no match for",
        };
    }
}
