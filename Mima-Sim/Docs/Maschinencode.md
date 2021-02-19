# Maschinencode

Der Maschinencode wird direkt in Hexadezimalzahlen geschrieben. Es werden jeweils 2 Ziffern zu einem Block zusammengefasst. Es können zur Formatierung Tabulatoren und neue Zeilen verwendet werden.

Beispiel:

```
04 2A 00 /* Lade 42 in Accumulator */
40 01 02 /* Verschiebe Wert aus Accumulator in Register X */
04 03 00 /* Lade 3 in Accumulator */
40 01 03 /* Verschiebe Wert aus Accumulator in Register Y */
08
```

Es kann jederzeit Code durch /* .. */ kommentiert werden, wobei .. ein beliebiger Kommentar ist

Registerzuordnungen:

Accumulator = 01
X = 02
Y = 03
Z = 04
IAR = 05
SAR = 06
SDR = 07

Instruktionen

load = 04 /* Lädt eine Konstante in den Accumulator */
mov = 40 /* Verschiebt einen Wert aus einem Register in ein anderes Register */

add = 08 /* Addiert die Werte aus den Registers X/Y und speichert das Ergebnis im Accumulator  */
sub = 13 /* Subtrahiert die Werte aus den Registers X/Y und speichert das Ergebnis im Accumulator  */
mul = 12 /* Multipliziert die Werte aus den Registers X/Y und speichert das Ergebnis im Accumulator  */
div = 14 /* Dividiert die Werte aus den Registers X/Y und speichert das Ergebnis im Accumulator  */

inc = 15 /* Incrementiert den Wert, der im Accumulator steht  */
dec = 16 /* Decrementiert den Wert, der im Accumulator steht  */