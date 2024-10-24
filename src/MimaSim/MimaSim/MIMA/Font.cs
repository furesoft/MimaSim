using System;
using System.Collections.Generic;
using MimaSim.MIMA.Components;

namespace MimaSim.MIMA;

public class Font
{
    Dictionary<char, (bool[] pixels, int width)> _characters = new();

    public (bool[] pixels, int width) this[char key] => _characters[key];

    public Font()
    {
        _characters.Add('a', ([
            false, true,  true,  true,  false,  // Zeile 1: .###.
            false, false, false, false, true,   // Zeile 2: ....#
            false, true,  true,  true,  true,   // Zeile 3: .####
            true,  false, false, false, true,   // Zeile 4: #...#
            false, true,  true, true, false      // Zeile 5: .###.
        ], 5));

        _characters.Add('b', ([
            true,  false, false,  false, false,   // Zeile 1: #..
            true,  false, false, false,  false,   // Zeile 2: #....
            true,  true,  true,  true,  false,    // Zeile 3: ####.
            true,  false, false, false, true,     // Zeile 4: #...#
            true,  true,  true,  true,  false     // Zeile 5: ####.
        ], 5));

        _characters.Add('c', ([
            false, true,  true,  true,  false,   // Zeile 1: .###.
            true,  false, false, false, false,   // Zeile 2: #....
            true,  false, false, false, false,   // Zeile 3: #....
            true,  false, false, false, false,   // Zeile 4: #....
            false, true,  true,  true,  false    // Zeile 5: .###.
        ], 5));

        _characters.Add('d', ([
            false, false, true,  true,  false,  // Zeile 1: ..##.
            false, true,  false, false, true,   // Zeile 2: .#..#
            false, true,  false, false, true,   // Zeile 3: .#..#
            false, true,  false, false, true,   // Zeile 4: .#..#
            false, false, true,  true,  false   // Zeile 5: ..##.
        ], 5));

        _characters.Add('e', ([
            false, true,  true,  true,  false,  // Zeile 1: .###.
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  true,  true,  true,  false,  // Zeile 3: ####.
            true,  false, false, false, false,  // Zeile 4: #....
            false, true,  true,  true,  false   // Zeile 5: .###.
        ], 5));

        _characters.Add('f', ([
            false, true,  true,  true,  true,   // Zeile 1: .####
            true,  false, false, false, false,  // Zeile 2: #....
            true,  true,  true,  false, false,  // Zeile 3: ###..
            true,  false, false, false, false,  // Zeile 4: #....
            true,  false, false, false, false   // Zeile 5: #....
        ], 5));

        _characters.Add('g', ([
            false, true,  true,  true,  false,  // Zeile 1: .###.
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  false, false, false, true,   // Zeile 3: #...#
            false, true,  true,  true,  true,   // Zeile 4: .####
            false, false, false, false, true    // Zeile 5: ....#
        ], 5));

        _characters.Add('h', ([
            true,  false, false, false, false,    // Zeile 1: #....
            true,  false, false, false, false,    // Zeile 2: #....
            true,  true,  true,  true,  false,    // Zeile 3: ####.
            true,  false, false, false, true,     // Zeile 4: #...#
            true,  false, false, false, true      // Zeile 5: #...#
        ], 5));

        _characters.Add('i', ([
            false, true, false,  // Zeile 1: .#.
            false, true, false,  // Zeile 2: .#.
            false, true, false,  // Zeile 3: .#.
            false, true, false,  // Zeile 4: .#.
            false, true, false   // Zeile 5: .#.
        ], 3));

        _characters.Add('j', ([
            false, false, false,  true,  false,  // Zeile 1: ...#.
            false, false, false,  true,  false,  // Zeile 2: ...#.
            false, false, false,  true,  false,  // Zeile 3: ...#.
            true,  false, false,  true,  false,  // Zeile 4: #..#.
            false,  true,  true,  false, false   // Zeile 5: .##..
        ], 5));

        _characters.Add('k', ([
            true,  false, false, false, true,   // Zeile 1: #...#
            true,  false, false, true,  false,  // Zeile 2: #..#.
            true,  true,  true,  false, false,  // Zeile 3: ###..
            true,  false, false, true,  false,  // Zeile 4: #..#.
            true,  false, false, false, true    // Zeile 5: #...#
        ], 5));

        _characters.Add('l', ([
            true,  false, false, false, false,  // Zeile 1: #....
            true,  false, false, false, false,  // Zeile 2: #....
            true,  false, false, false, false,  // Zeile 3: #....
            true,  false, false, false, false,  // Zeile 4: #....
            true,  true,  true,  true,  false   // Zeile 5: ####.
        ], 5));

        _characters.Add('m', ([
            true,  true,  false, true,  true,   // Zeile 1: ##.##
            true,  false, true,  false, true,   // Zeile 2: #.#.#
            true,  false, false, false, true,   // Zeile 3: #...#
            true,  false, false, false, true,   // Zeile 4: #...#
            true,  false, false, false, true    // Zeile 5: #...#
        ], 5));

        _characters.Add('n', ([
            true,  true,  true,  false, false,  // Zeile 1: ###..
            true,  false, false, true,  false,  // Zeile 2: #..#.
            true,  false, false, false, true,   // Zeile 3: #...#
            true,  false, false, false, true,   // Zeile 4: #...#
            true,  false, false, false, true    // Zeile 5: #...#
        ], 5));

        _characters.Add('o', ([
            false, true,  true,  true,  false,  // Zeile 1: .###.
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  false, false, false, true,   // Zeile 3: #...#
            true,  false, false, false, true,   // Zeile 4: #...#
            false, true,  true,  true,  false   // Zeile 5: .###.
        ], 5));

        _characters.Add('p', ([
            true,  true,  true,  true,  false,  // Zeile 1: ####.
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  true,  true,  true,  false,  // Zeile 3: ####.
            true,  false, false, false, false,  // Zeile 4: #....
            true,  false, false, false, false   // Zeile 5: #....
        ], 5));

        _characters.Add('q', ([
            false, true,  true,  true,  false,  // Zeile 1: .###.
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  false, false, false, true,   // Zeile 3: #...#
            false, true,  true,  true,  true,   // Zeile 4: .####
            false, false, false, false, true    // Zeile 5: ....#
        ], 5));

        _characters.Add('r', ([
            true,  true,  true,  false, false,  // Zeile 1: ###..
            true,  false, false, true,  false,  // Zeile 2: #..#.
            true,  false, false, false, false,  // Zeile 3: #....
            true,  false, false, false, false,  // Zeile 4: #....
            true,  false, false, false, false   // Zeile 5: #....
        ], 5));

        _characters.Add('s', ([
            false, true,  true,  true,  false,  // Zeile 1: .###.
            true,  false, false, false, false,  // Zeile 2: #....
            false, true,  true,  true,  false,  // Zeile 3: .###.
            false, false, false, false, true,   // Zeile 4: ....#
            true,  true,  true,  true,  false   // Zeile 5: ####.
        ], 5));

        _characters.Add('t', ([
            true,  true,  true,  true,  true,    // Zeile 1: #####
            false, false, true,  false, false,   // Zeile 2: ..#..
            false, false, true,  false, false,   // Zeile 3: ..#..
            false, false, true,  false, false,   // Zeile 4: ..#..
            false, false, true,  false,  false   // Zeile 5: ..#..
        ], 5));

        _characters.Add('u', ([
            true,  false, false, false, true,   // Zeile 1: #...#
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  false, false, false, true,   // Zeile 3: #...#
            true,  false, false, false, true,   // Zeile 4: #...#
            false, true,  true,  true,  false   // Zeile 5: .###.
        ], 5));

        _characters.Add('v', ([
            true,  false, false, false, true,   // Zeile 1: #...#
            true,  false, false, false, true,   // Zeile 2: #...#
            false, true,  false, true,  false,  // Zeile 3: .#.#.
            false, true,  false, true,  false,  // Zeile 4: .#.#.
            false, false, true,  false, false   // Zeile 5: ..#..
        ], 5));

        _characters.Add('w', ([
            true,  false, false, false, true,   // Zeile 1: #...#
            true,  false, false, false, true,   // Zeile 2: #...#
            true,  false, false, false, true,   // Zeile 3: #...#
            true,  false, true,  false, true,   // Zeile 4: #.#.#
            false, true,  false, true,  false   // Zeile 5: .#.#.
        ], 5));

        _characters.Add('x', ([
            true,  false, false, false, true,   // Zeile 1: #...#
            false, true,  false, true,  false,  // Zeile 2: .#.#.
            false, false, true,  false, false,  // Zeile 3: ..#..
            false, true,  false, true,  false,  // Zeile 4: .#.#.
            true,  false, false, false, true    // Zeile 5: #...#
        ], 5));

        _characters.Add('y', ([
            true,  false, false, false, true,   // Zeile 1: #...#
            false, true,  false, true,  false,  // Zeile 2: .#.#.
            false, false, true,  false, false,  // Zeile 3: ..#..
            false, false, true,  false, false,  // Zeile 4: ..#..
            false, true,  true,  false, false   // Zeile 5: .##..
        ], 5));

        _characters.Add('z', ([
            true,  true,  true,  true,  true,   // Zeile 1: #####
            false, false, false, true,  false,  // Zeile 2: ...#.
            false, false, true,  false, false,  // Zeile 3: ..#..
            false, true,  false, false, false,  // Zeile 4: .#...
            true,  true,  true,  true,  true    // Zeile 5: #####
        ], 5));
    }

    public void DrawChar()
    {
        var xOffset = CPU.Instance.Display.DX.GetValueWithoutNotification();
        var yOffset = CPU.Instance.Display.DY.GetValueWithoutNotification();
        var ch = (char)CPU.Instance.Display.DC.GetValueWithoutNotification();

        if (!_characters.TryGetValue(ch, out var character))
        {
            throw new NotImplementedException($"unknown character {ch}");
        }

        var (pixels, width) = character;
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