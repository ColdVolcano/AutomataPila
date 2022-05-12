// See https://aka.ms/new-console-template for more information

var automaton = new PushdownAutomaton(new List<string>
    {
        "q0",
        "q1",
        "q2",
        "q3",
        "q4",
        "q5",
        "q6",
        "q7",
    },
    new List<char>
    {
        '1',
        '2',
        '3',
        '4',
        '5',
        'a',
        'b',
        'c',
        'A',
        'B',
        'C',
        'D',
        'E',
        'F',
        '|',
    },
    new List<string>
    {
        "C",
        "J",
        "F",
        "T",
        "K",
        "@",
    },
    new Dictionary<TransitionOrigin, TransitionDestination>
    {
        { new("q0", '1', null), new("q1", new List<string> { "C" }) },
        { new("q0", '2', null), new("q2", new List<string> { "J" }) },
        { new("q0", '3', null), new("q3", new List<string> { "F" }) },
        { new("q0", '4', null), new("q4", new List<string> { "T" }) },
        { new("q0", '5', null), new("q5", new List<string> { "K" }) },

        { new("q1", 'a', "C"), new("q1", new List<string> { "C", "J" }) },
        { new("q1", 'b', "C"), new("q3", new List<string> { "F", "C" }) },
        { new("q1", 'c', "C"), new("q1", new List<string> { "C", "T" }) },
        { new("q1", 'A', "C"), new("q6", null) },
        { new("q1", 'B', "C"), new("q6", null) },
        { new("q1", 'C', "C"), new("q6", null) },
        { new("q1", 'D', "C"), new("q6", null) },
        { new("q1", 'E', "C"), new("q6", null) },
        { new("q1", 'F', "C"), new("q6", null) },

        { new("q2", 'a', "J"), new("q2", new List<string> { "J", "J" }) },
        { new("q2", 'b', "J"), new("q2", new List<string> { "J", "F" }) },
        { new("q2", 'c', "J"), new("q3", new List<string> { "F", "C" }) },
        { new("q2", 'A', "J"), new("q6", null) },
        { new("q2", 'B', "J"), new("q6", null) },
        { new("q2", 'C', "J"), new("q6", null) },
        { new("q2", 'D', "J"), new("q6", null) },
        { new("q2", 'E', "J"), new("q6", null) },
        { new("q2", 'F', "J"), new("q6", null) },

        { new("q3", 'a', "F"), new("q3", new List<string> { "F", "C" }) },
        { new("q3", 'b', "F"), new("q3", new List<string> { "F", "J" }) },
        { new("q3", 'c', "F"), new("q3", new List<string> { "F", "K" }) },
        { new("q3", 'A', "F"), new("q6", null) },
        { new("q3", 'B', "F"), new("q6", null) },
        { new("q3", 'C', "F"), new("q6", null) },
        { new("q3", 'D', "F"), new("q6", null) },
        { new("q3", 'E', "F"), new("q6", null) },
        { new("q3", 'F', "F"), new("q6", null) },

        { new("q4", 'a', "T"), new("q1", new List<string> { "C", "T" }) },
        { new("q4", 'b', "T"), new("q3", new List<string> { "F", "T" }) },
        { new("q4", 'c', "T"), new("q4", new List<string> { "T" }) },
        { new("q4", 'A', "T"), new("q6", null) },
        { new("q4", 'B', "T"), new("q6", null) },
        { new("q4", 'C', "T"), new("q6", null) },
        { new("q4", 'D', "T"), new("q6", null) },
        { new("q4", 'E', "T"), new("q6", null) },
        { new("q4", 'F', "T"), new("q6", null) },

        { new("q5", 'a', "K"), new("q1", new List<string> { "C", "K" }) },
        { new("q5", 'b', "K"), new("q5", new List<string> { "K", "T" }) },
        { new("q5", 'c', "K"), new("q3", new List<string> { "F", "K" }) },
        { new("q5", 'A', "K"), new("q6", null) },
        { new("q5", 'B', "K"), new("q6", null) },
        { new("q5", 'C', "K"), new("q6", null) },
        { new("q5", 'D', "K"), new("q6", null) },
        { new("q5", 'E', "K"), new("q6", null) },
        { new("q5", 'F', "K"), new("q6", null) },

        { new("q6", null, "C"), new("q1", new List<string> { "C" }) },
        { new("q6", null, "J"), new("q2", new List<string> { "J" }) },
        { new("q6", null, "F"), new("q3", new List<string> { "F" }) },
        { new("q6", null, "T"), new("q4", new List<string> { "T" }) },
        { new("q6", null, "K"), new("q5", new List<string> { "K" }) },
        { new("q6", '|', "@"), new("q7", null) },
    },
    "q0",
    "@",
    new List<string>
    {
        "q7",
    });
do
{
    Console.WriteLine("Ingresa la palabra a comprobar");
    Console.WriteLine(automaton.ValidForInput(Console.ReadLine()) ? "La palabra si es aceptada por el automata" : "La palabra no es aceptada por el automata");
} while (true);

public class PushdownAutomaton
{
    private readonly string? initialStackSymbol;
    private Stack<string> stack;
    private string? StackTop => stack.Count == 0 ? null : stack.Peek();

    private readonly IReadOnlyList<string> states;

    private readonly string initialState;

    private string currentState;

    private readonly IReadOnlyList<char> inputAlphabet;

    private readonly IReadOnlyDictionary<TransitionOrigin, TransitionDestination> transitionRelation;

    private readonly IReadOnlyList<string> acceptingStates;

    public PushdownAutomaton(IReadOnlyList<string> states,
        IReadOnlyList<char> inputAlphabet,
        IReadOnlyList<string> stackAlphabet,
        IReadOnlyDictionary<TransitionOrigin, TransitionDestination> transitionRelation,
        string initialState,
        string? stackState,
        IReadOnlyList<string> acceptingStates)
    {
        this.states = new List<string>(states);

        if (!states.Contains(initialState))
            throw new ArgumentException($"Tried to initialize automaton state with non-existent state ({initialState})", nameof(initialState));

        this.initialState = initialState;

        this.inputAlphabet = new List<char>(inputAlphabet);

        if (stackState != null)
        {
            if (!stackAlphabet.Contains(stackState))
                throw new ArgumentException($"Tried to initialize stack state with symbol not in stackLanguage ({stackState})", nameof(stackState));
        }

        initialStackSymbol = stackState;

        foreach (var tup in transitionRelation)
        {
            if (tup.Key.Input != null && !inputAlphabet.Contains(tup.Key.Input.Value))
                throw new ArgumentException("Bad Input on transition");
            if (!states.Contains(tup.Key.State) || !states.Contains(tup.Value.State))
                throw new ArgumentException("Bad State on transition");
            if (tup.Key.StackTop != null && !stackAlphabet.Contains(tup.Key.StackTop) ||
                tup.Value.StackTop != null && tup.Value.StackTop.Any(s => !stackAlphabet.Contains(s)))
                throw new ArgumentException("Bad StackTop on transition");
        }

        this.transitionRelation = new Dictionary<TransitionOrigin, TransitionDestination>(transitionRelation);

        foreach (string s in acceptingStates)
            if (!states.Contains(initialState))
                throw new ArgumentException($"Tried to add non-existent accepting state ({s})", nameof(acceptingStates));

        this.acceptingStates = new List<string>(acceptingStates);
    }

    private void printCurrentState(string remainingInput)
    {
        string[] bak = new string[stack.Count];
        stack.CopyTo(bak, 0);

        Console.WriteLine($"({currentState}, {(remainingInput.Length == 0 ? "λ" : remainingInput)}, {(bak.Length == 0 ? "λ" : string.Concat(bak))})");
    }

    public bool ValidForInput(string input)
    {
        if (!input.All(c => inputAlphabet.Contains(c)))
            throw new ArgumentException("No input of this automaton coincides with given input", nameof(input));

        string modifiable = input;

        stack = new Stack<string>();

        if (initialStackSymbol != null)
            stack.Push(initialStackSymbol);

        currentState = initialState;

        printCurrentState(modifiable);

        int consumed;

        while ((consumed = AdvanceWithInput(modifiable.Length == 0 ? null : modifiable[0])) >= 0)
        {
            modifiable = modifiable[consumed..];

            Console.Write('├');
            printCurrentState(modifiable);

            if (modifiable.Length == 0 && acceptingStates.Contains(currentState))
                return true;
        }

        return modifiable.Length == 0 && acceptingStates.Contains(currentState);
    }

    private int AdvanceWithInput(char? input)
    {
        for (int i = input == null ? 2 : 0; i < 4; i++)
        {
            var origin = new TransitionOrigin(currentState, i < 2 ? input : null, i % 2 == 0 ? StackTop : null);

            if (!transitionRelation.TryGetValue(origin, out var destination))
                continue;

            currentState = destination.State;

            if (i % 2 == 0 && StackTop != null)
                stack.Pop();

            if (destination.StackTop != null)
                foreach (var symbol in destination.StackTop.Reverse())
                    stack.Push(symbol);

            return i < 2 ? 1 : 0;
        }

        return -1;
    }
}

public record TransitionOrigin(string State, char? Input, string? StackTop);

public record TransitionDestination(string State, IReadOnlyList<string>? StackTop);