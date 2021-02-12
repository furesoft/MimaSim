# Maschinencode

Der Maschinencode wird direkt in Hexadezimalzahlen geschrieben. Es werden jeweils 2 Ziffern zu einem Block zusammengefasst. Es können zur Formatierung Tabulatoren und neue Zeilen verwendet werden.

Beispiel:

```
10 2A 00 02 /* setze x */
10 01 00 03 /* setze y */
14 /* Addiere beide zahlen */
```

Es kann jederzeit Code durch /* .. */ kommentiert werden, wobei .. ein beliebiger Kommentar ist

Registerzuordnungen:
Modus: ```r = read, w = write, rw = readwrite```

|Register|Byte|Modus|
|--------|----|-----|
|Accumulator|01|rw|
|X|02|rw|
|Y|03|rw|
|Z|04|rw|
|IR|05|r|
|IAR|06|rw|
|SAR|07|rw|
|SDR|08|rw|

Instruktionen

|Befehl |Opcode|Argumente|Anzahl Argumente|Beschreibung|
|-------|------|---------|----------------|------------|
|mov    |2A    |Register und/oder Speicherstelle |2|Werte verschieben|
|loadi  |2B    |Konstante|1|Lädt eine Konstante in das Register Accumulator|
|jmp    |3A    |Absolute Adresse|1|Springt zur Adresse|
|jmpr   |3B    |Relative Adresse|1|Springt soweit vor/zurück wie im Argument angegeben|
|jmpc   |3C    |Absolute Adresse|1|Springt, wenn Accumulator 1 ist zur Adresse|
|jmpa   |3D    |Speicheradresse|1|Lädt Adresse aus dem Speicher und springt zu der Adresse, wie im Speicher angegeben|
|cmpe   |4A    |Konstante             |1|Prüft ob Wert im Accumulator gleich wie Konstante ist|
|cmpne  |4B    |Konstante|1|Prüft ob Wert im Accumulator nicht gleich ist|
|cmplt  |4C    |Konstante|1|Prüft ob Wert im Accumulator kleiner ist|
|cmpgt  |4D    |Konstante|1|Prüft ob Wert im Accumulator größer ist|
|add    |5A    |-|0|Addiert die Werte von Register X und Y  und speichert das Ergebnis im Accumulator|
|sub    |5A    |-|0|Subtrahiert die Werte von Register X und Y  und speichert das Ergebnis im Accumulator|