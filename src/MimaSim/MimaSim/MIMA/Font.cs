using System.Linq;
using System.Collections.Generic;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA;

public class Font
{
    private static readonly Dictionary<char, (bool[] pixels, int width)> Characters = new();

    public Font()
    {
        AddLowerCaseLetters();
        AddDigits();
        AddSpecialChars();
    }

    private void AddSpecialChars()
    {
        Characters.Add('!', ([
            false, false, true, false, false,   // Zeile 1: ..#..
            false, false, true, false, false,   // Zeile 2: ..#..
            false, false, true, false, false,   // Zeile 3: ..#..
            false, false, false, false, false,  // Zeile 4: .....
            false, false, true, false, false    // Zeile 5: ..#..
        ], 5));

        Characters.Add('@', ([
            true,  true,  true,  true,  false,  // Zeile 1: ####.
            true,  false, false, true,  true,   // Zeile 2: #..##
            true,  false, true,  false, true,   // Zeile 3: #.#.#
            true,  false, true,  true,  false,  // Zeile 4: #.##.
            true,  true,  false, false, false   // Zeile 5: ##...
        ], 5));

        Characters.Add('#', ([
            false, true,  false, true,  false,  // Zeile 1: .#.#.
            true,  true,  true,  true,  true,   // Zeile 2: #####
            false, true,  false, true,  false,  // Zeile 3: .#.#.
            true,  true,  true,  true,  true,   // Zeile 4: #####
            false, true,  false, true,  false   // Zeile 5: .#.#.
        ], 5));

        Characters.Add('$', ([
            false, true,  true,  true,  false,  // Zeile 1: .###.
            true,  false, true,  false, false,  // Zeile 2: #.#..
            false, true,  true,  true,  false,  // Zeile 3: .###.
            false, false, true,  false, true,   // Zeile 4: ..#.#
            true,  true,  true,  true, false    // Zeile 5: ####.
        ], 5));

        Characters.Add('%', ([
            true,  false, false, true,  false,  // Zeile 1: #..#.
            false, false, true,  false, false,  // Zeile 2: ..#..
            false, true,  false, false, false,  // Zeile 3: .#...
            false, false, true,  false, false,  // Zeile 4: ..#..
            true,  false, false, true,  false   // Zeile 5: #..#.
        ], 5));

        Characters.Add('&', ([
            false, true,  true,  false, false,  // Zeile 1: .##..
            true,  false, true,  false, false,  // Zeile 2: #.#..
            false, true,  true,  false, true,   // Zeile 3: .##.#
            true,  false, true,  true,  false,  // Zeile 4: #.##.
            false, true,  false, true,  true    // Zeile 5: .#.##
        ], 5));

        Characters.Add('*', ([
            false, true,  false, true,  false,  // Zeile 1: .#.#.
            false, false, true, false, false,   // Zeile 2: ..#..
            true,  true,  true,  true,  true,   // Zeile 3: #####
            false, false, true, false, false,   // Zeile 4: ..#..
            false, true,  false, true,  false   // Zeile 5: .#.#.
        ], 5));

        Characters.Add('+', ([
            false, false, true, false, false,   // Zeile 1: ..#..
            false, false, true, false, false,   // Zeile 2: ..#..
            true,  true,  true,  true,  true,   // Zeile 3: #####
            false, false, true, false, false,   // Zeile 4: ..#..
            false, false, true, false, false    // Zeile 5: ..#..
        ], 5));

        Characters.Add('-', ([
            false, false, false, false, false,  // Zeile 1: .....
            false, false, false, false, false,  // Zeile 2: .....
            true,  true,  true,  true,  true,   // Zeile 3: #####
            false, false, false, false, false,  // Zeile 4: .....
            false, false, false, false, false   // Zeile 5: .....
        ], 5));

        Characters.Add('/', ([
            false, false, false, false, true,   // Zeile 1: ....#
            false, false, false, true,  false,  // Zeile 2: ...#.
            false, false, true,  false, false,  // Zeile 3: ..#..
            false, true,  false, false, false,  // Zeile 4: .#...
            true,  false, false, false, false   // Zeile 5: #....
        ], 5));

        Characters.Add('=', ([
            false, false, false, false, false,  // Zeile 1: .....
            true,  true,  true,  true,  true,   // Zeile 2: #####
            false, false, false, false, false,  // Zeile 3: .....
            true,  true,  true,  true,  true,   // Zeile 4: #####
            false, false, false, false, false   // Zeile 5: .....
        ], 5));

        Characters.Add(' ', ([
            false, false, false, false, false,  // Zeile 1: .....
            false, false, false, false, false,  // Zeile 2: .....
            false, false, false, false, false,  // Zeile 3: .....
            false, false, false, false, false,  // Zeile 4: .....
            false, false, false, false, false,  // Zeile 5: .....
        ], 5));
    }

    private void AddDigits()
    {
        Characters.Add('0', ([
            true,  true,  true,  true,  true,  // Zeile 1: #####
            true,  false, false, false, true,  // Zeile 2: #...#
            true,  false, false, false, true,  // Zeile 3: #...#
            true,  false, false, false, true,  // Zeile 4: #...#
            true,  true,  true,  true,  true   // Zeile 5: #####
        ], 5));

        Characters.Add('1', ([
            false, false, false, true,  false,  // Zeile 1: ...#.
            false, false, true,  true,  false,  // Zeile 2: ..##.
            false, false, false, true,  false,  // Zeile 3: ...#.
            false, false, false, true,  false,  // Zeile 4: ...#.
            false, false, false, true,  false   // Zeile 5: ...#.
        ], 5));

        Characters.Add('2', ([
            true,  true,  true,  true,  true,   // Zeile 1: #####
            false, false, false, false, true,   // Zeile 2: ....#
            true,  true,  true,  true,  true,   // Zeile 3: #####
            true,  false, false, false, false,  // Zeile 4: #....
            true,  true,  true,  true,  true    // Zeile 5: #####
        ], 5));

        Characters.Add('3', ([
            true,  true,  true,  true,  true,   // Zeile 1: #####
            false, false, false, false, true,   // Zeile 2: ....#
            true,  true,  true,  true,  true,   // Zeile 3: #####
            false, false, false, false, true,   // Zeile 4: ....#
            true,  true,  true,  true,  true    // Zeile 5: #####
        ], 5));

        Characters.Add('4', ([
            true,  false, false, false, true,   // Zeile 1: #...#
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  true,  true,  true,  true,   // Zeile 3: #####
            false, false, false, false, true,   // Zeile 4: ....#
            false, false, false, false, true    // Zeile 5: ....#
        ], 5));

        Characters.Add('5', ([
            true,  true,  true,  true,  true,   // Zeile 1: #####
            true,  false, false, false, false,  // Zeile 2: #....
            true,  true,  true,  true,  true,   // Zeile 3: #####
            false, false, false, false, true,   // Zeile 4: ....#
            true,  true,  true,  true,  true    // Zeile 5: #####
        ], 5));

        Characters.Add('6', ([
            true,  true,  true,  true,  true,   // Zeile 1: #####
            true,  false, false, false, false,  // Zeile 2: #....
            true,  true,  true,  true,  true,   // Zeile 3: #####
            true,  false, false, false, true,   // Zeile 4: #...#
            true,  true,  true,  true,  true    // Zeile 5: #####
        ], 5));

        Characters.Add('7', ([
            true,  true,  true,  true,  true,   // Zeile 1: #####
            false, false, false, false, true,   // Zeile 2: ....#
            false, false, false, true,  false,  // Zeile 3: ...#.
            false, false, true,  false, false,  // Zeile 4: ..#..
            false, true,  false, false, false   // Zeile 5: .#...
        ], 5));

        Characters.Add('8', ([
            true,  true,  true,  true,  true,   // Zeile 1: #####
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  true,  true,  true,  true,   // Zeile 3: #####
            true,  false, false, false, true,   // Zeile 4: #...#
            true,  true,  true,  true,  true    // Zeile 5: #####
        ], 5));

        Characters.Add('9', ([
            true,  true,  true,  true,  true,   // Zeile 1: #####
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  true,  true,  true,  true,   // Zeile 3: #####
            false, false, false, false, true,   // Zeile 4: ....#
            true,  true,  true,  true,  true    // Zeile 5: #####
        ], 5));
    }

    private void AddLowerCaseLetters()
    {
        Characters.Add('a', ([
            false, true,  true,  true,  false,  // Zeile 1: .###.
            false, false, false, false, true,   // Zeile 2: ....#
            false, true,  true,  true,  true,   // Zeile 3: .####
            true,  false, false, false, true,   // Zeile 4: #...#
            false, true,  true, true, false      // Zeile 5: .###.
        ], 5));

        Characters.Add('b', ([
            true,  false, false,  false, false,   // Zeile 1: #..
            true,  false, false, false,  false,   // Zeile 2: #....
            true,  true,  true,  true,  false,    // Zeile 3: ####.
            true,  false, false, false, true,     // Zeile 4: #...#
            true,  true,  true,  true,  false     // Zeile 5: ####.
        ], 5));

        Characters.Add('c', ([
            false, true,  true,  true,  false,   // Zeile 1: .###.
            true,  false, false, false, false,   // Zeile 2: #....
            true,  false, false, false, false,   // Zeile 3: #....
            true,  false, false, false, false,   // Zeile 4: #....
            false, true,  true,  true,  false    // Zeile 5: .###.
        ], 5));

        Characters.Add('d', ([
            false, false, false,  false, true,   // Zeile 1: ....#
            false, false,  false, false, true,   // Zeile 2: ....#
            false, true, true, true,    true,    // Zeile 3: .####
            false, true,  false, false, true,    // Zeile 4: .#..#
            false, false, true,  true,  false    // Zeile 5: ..##.
        ], 5));

        Characters.Add('e', ([
            false, true,  true,  true,  false,  // Zeile 1: .###.
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  true,  true,  true,  false,  // Zeile 3: ####.
            true,  false, false, false, false,  // Zeile 4: #....
            false, true,  true,  true,  false   // Zeile 5: .###.
        ], 5));

        Characters.Add('f', ([
            false, true,  true,  true,  true,   // Zeile 1: .####
            true,  false, false, false, false,  // Zeile 2: #....
            true,  true,  true,  false, false,  // Zeile 3: ###..
            true,  false, false, false, false,  // Zeile 4: #....
            true,  false, false, false, false   // Zeile 5: #....
        ], 5));

        Characters.Add('g', ([
            false, true,  true,  true,  false,  // Zeile 1: .###.
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  false, false, false, true,   // Zeile 3: #...#
            false, true,  true,  true,  true,   // Zeile 4: .####
            false, false, false, false, true    // Zeile 5: ....#
        ], 5));

        Characters.Add('h', ([
            true,  false, false, false, false,    // Zeile 1: #....
            true,  false, false, false, false,    // Zeile 2: #....
            true,  true,  true,  true,  false,    // Zeile 3: ####.
            true,  false, false, false, true,     // Zeile 4: #...#
            true,  false, false, false, true      // Zeile 5: #...#
        ], 5));

        Characters.Add('i', ([
            false, true, false,  // Zeile 1: .#.
            false, true, false,  // Zeile 2: .#.
            false, true, false,  // Zeile 3: .#.
            false, true, false,  // Zeile 4: .#.
            false, true, false   // Zeile 5: .#.
        ], 3));

        Characters.Add('j', ([
            false, false, false,  true,  false,  // Zeile 1: ...#.
            false, false, false,  true,  false,  // Zeile 2: ...#.
            false, false, false,  true,  false,  // Zeile 3: ...#.
            true,  false, false,  true,  false,  // Zeile 4: #..#.
            false,  true,  true,  false, false   // Zeile 5: .##..
        ], 5));

        Characters.Add('k', ([
            true,  false, false, false, true,   // Zeile 1: #...#
            true,  false, false, true,  false,  // Zeile 2: #..#.
            true,  true,  true,  false, false,  // Zeile 3: ###..
            true,  false, false, true,  false,  // Zeile 4: #..#.
            true,  false, false, false, true    // Zeile 5: #...#
        ], 5));

        Characters.Add('l', ([
            true,  false, false, false, false,  // Zeile 1: #....
            true,  false, false, false, false,  // Zeile 2: #....
            true,  false, false, false, false,  // Zeile 3: #....
            true,  false, false, false, false,  // Zeile 4: #....
            true,  true,  true,  true,  false   // Zeile 5: ####.
        ], 5));

        Characters.Add('m', ([
            true,  true,  false, true,  true,   // Zeile 1: ##.##
            true,  false, true,  false, true,   // Zeile 2: #.#.#
            true,  false, false, false, true,   // Zeile 3: #...#
            true,  false, false, false, true,   // Zeile 4: #...#
            true,  false, false, false, true    // Zeile 5: #...#
        ], 5));

        Characters.Add('n', ([
            true,  true,  true,  false, false,  // Zeile 1: ###..
            true,  false, false, true,  false,  // Zeile 2: #..#.
            true,  false, false, false, true,   // Zeile 3: #...#
            true,  false, false, false, true,   // Zeile 4: #...#
            true,  false, false, false, true    // Zeile 5: #...#
        ], 5));

        Characters.Add('o', ([
            false, true,  true,  true,  false,  // Zeile 1: .###.
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  false, false, false, true,   // Zeile 3: #...#
            true,  false, false, false, true,   // Zeile 4: #...#
            false, true,  true,  true,  false   // Zeile 5: .###.
        ], 5));

        Characters.Add('p', ([
            true,  true,  true,  true,  false,  // Zeile 1: ####.
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  true,  true,  true,  false,  // Zeile 3: ####.
            true,  false, false, false, false,  // Zeile 4: #....
            true,  false, false, false, false   // Zeile 5: #....
        ], 5));

        Characters.Add('q', ([
            false, true,  true,  true,  false,  // Zeile 1: .###.
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  false, false, false, true,   // Zeile 3: #...#
            false, true,  true,  true,  true,   // Zeile 4: .####
            false, false, false, false, true    // Zeile 5: ....#
        ], 5));

        Characters.Add('r', ([
            true,  true,  true,  false, false,  // Zeile 1: ###..
            true,  false, false, true,  false,  // Zeile 2: #..#.
            true,  false, false, false, false,  // Zeile 3: #....
            true,  false, false, false, false,  // Zeile 4: #....
            true,  false, false, false, false   // Zeile 5: #....
        ], 5));

        Characters.Add('s', ([
            false, true,  true,  true,  false,  // Zeile 1: .###.
            true,  false, false, false, false,  // Zeile 2: #....
            false, true,  true,  true,  false,  // Zeile 3: .###.
            false, false, false, false, true,   // Zeile 4: ....#
            true,  true,  true,  true,  false   // Zeile 5: ####.
        ], 5));

        Characters.Add('t', ([
            true,  true,  true,  true,  true,    // Zeile 1: #####
            false, false, true,  false, false,   // Zeile 2: ..#..
            false, false, true,  false, false,   // Zeile 3: ..#..
            false, false, true,  false, false,   // Zeile 4: ..#..
            false, false, true,  false,  false   // Zeile 5: ..#..
        ], 5));

        Characters.Add('u', ([
            true,  false, false, false, true,   // Zeile 1: #...#
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  false, false, false, true,   // Zeile 3: #...#
            true,  false, false, false, true,   // Zeile 4: #...#
            false, true,  true,  true,  false   // Zeile 5: .###.
        ], 5));

        Characters.Add('v', ([
            true,  false, false, false, true,   // Zeile 1: #...#
            true,  false, false, false, true,   // Zeile 2: #...#
            false, true,  false, true,  false,  // Zeile 3: .#.#.
            false, true,  false, true,  false,  // Zeile 4: .#.#.
            false, false, true,  false, false   // Zeile 5: ..#..
        ], 5));

        Characters.Add('w', ([
            true,  false, false, false, true,   // Zeile 1: #...#
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  false, false, false, true,   // Zeile 3: #...#
            true,  false, true,  false, true,   // Zeile 4: #.#.#
            false, true,  false, true,  false   // Zeile 5: .#.#.
        ], 5));

        Characters.Add('x', ([
            true,  false, false, false, true,   // Zeile 1: #...#
            false, true,  false, true,  false,  // Zeile 2: .#.#.
            false, false, true,  false, false,  // Zeile 3: ..#..
            false, true,  false, true,  false,  // Zeile 4: .#.#.
            true,  false, false, false, true    // Zeile 5: #...#
        ], 5));

        Characters.Add('y', ([
            true,  false, false, false, true,   // Zeile 1: #...#
            false, true,  false, true,  false,  // Zeile 2: .#.#.
            false, false, true,  false, false,  // Zeile 3: ..#..
            false, false, true,  false, false,  // Zeile 4: ..#..
            false, true,  true,  false, false   // Zeile 5: .##..
        ], 5));

        Characters.Add('z', ([
            true,  true,  true,  true,  true,   // Zeile 1: #####
            false, false, false, true,  false,  // Zeile 2: ...#.
            false, false, true,  false, false,  // Zeile 3: ..#..
            false, true,  false, false, false,  // Zeile 4: .#...
            true,  true,  true,  true,  true    // Zeile 5: #####
        ], 5));
    }

    public (short width, short height) Measure(char c)
    {
        foreach (var kv in Characters)
        {
            if (kv.Key == c)
            {
                return ((short)kv.Value.Item2, (short)(kv.Value.Item1.Length / kv.Value.Item2));
            }
        }

        return (0, 0);
    }

    public short Measure(string s)
    {
        return (short)s.Sum(c => Measure(c).width + 1);
    }

    public void DrawChar(short xOffset, short yOffset, char ch)
    {
        if (!Characters.TryGetValue(ch, out var character))
        {
            bool[] boxPixels =
            [
                true, true, true, true, true, true,
                true, false, false, false, false, true,
                true, false, true, false, false, true,
                true, false, false, false, false, true,
                true, true, true, true, true, true
            ];

            DrawChar(6, boxPixels, yOffset, xOffset);
        }

        var (pixels, width) = character;
        DrawChar(width, pixels, yOffset, xOffset);
    }

    private static void DrawChar(int width, bool[] pixels, short yOffset, short xOffset)
    {
        for (var y = 0; y < 5; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (pixels[(y * width) + x])
                {
                    CPU.Instance.Display.SetPixel((short)(y + yOffset), (short)(x + xOffset), DisplayColor.Black);
                }
            }
        }
    }
}
