# Assemblycode
Der Assemblycode ist gewissermaßen genauso aufgebaut wie der Maschinencode, der Unterschied ist die Lesbarkeit.

Aufbau eines Befehls:
```
Mnemnonic Argument1, Argument2
```

Ein Beispiel lädt die Konstante 0x42 in das Register Accumulator:

```
mov 0x42, Accumulator
```

Möchte man auf einer Adresse im Speicher verweisen:

```mov A2, &42```

Der Wert A2 wird an die Speicherstelle 42 geschrieben

Es kann jederzeit Code durch /* .. */ kommentiert werden, wobei .. ein beliebiger Kommentar ist
