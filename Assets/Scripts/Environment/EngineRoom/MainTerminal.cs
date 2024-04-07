using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class MainTerminal : MonoBehaviour
{
    [SerializeField] private List<StabilizerTerminal> terminals;
    [SerializeField] private TextMeshProUGUI text;

    private int terminalsLeft;

    public event Action<StabilizersState> StabilizerBroken;

    private void Start()
    {
        foreach (StabilizerTerminal terminal in terminals)
        {
            terminal.Broked += OnStabilizerTerminalBroken;
            terminalsLeft += terminal.IsBroken ? 0 : 1;
        }
    }

    private void OnStabilizerTerminalBroken()
    {
        StringBuilder stringBuilder = new StringBuilder();

        terminalsLeft--;
        StabilizersState stabilizersState = (StabilizersState)(terminals.Count - terminalsLeft);
        StabilizerBroken?.Invoke(stabilizersState);
        var stateColor = GetColorState(stabilizersState);

        for (int i = 0; i < terminals.Count; i++)
        {
            var terminal = terminals[i];
            string state = terminal.IsBroken ? "<color=#ff0000>OFF</color>" : "<color=#00ff00>ON</color>";
            stringBuilder.AppendLine($"stabilizer {i+1}: {state}");
        }

        stringBuilder.Append($"engine state: <color={stateColor}>{stabilizersState}</color>");

        text.text = stringBuilder.ToString();
    }

    private string GetColorState(StabilizersState stabilizersState)
    {
        switch (stabilizersState)
        {
            case StabilizersState.OK:
                return "#00ff00";
            case StabilizersState.INCORRECT:
                return "#ffff00";
            case StabilizersState.UNSTABLE:
                return "#ffa200";
            case StabilizersState.BAD:
                return "#ff5500";
            case StabilizersState.EXTREME:
                return "#ff0000";
        }
        return null;
    }
}

public enum StabilizersState
{
    OK,
    INCORRECT,
    UNSTABLE,
    BAD,
    EXTREME
}
