using SSB.Domain.Enums;

namespace SSB.Domain.Entities;

public sealed class Script
{
    #region Properties & Variables
    //
    // private
    //
    private readonly List<string> _observations = [];
    //
    // public
    //
    public Status Status { get; init; }
    public string Group { get; init; }
    public string Path { get; init; }
    public IReadOnlyCollection<string> Observations => _observations.AsReadOnly();
    #endregion

    #region Constructor
    private Script() { }
    #endregion // Constructor

    #region Methods
    public void AddObservation(string observation)
    {
        if (!_observations.Contains(observation))
            _observations.Add(observation);
    }

    public void ClearObservations()
    {
        _observations.Clear();
    }
    #endregion // Methods

    public class ScriptBuilder
    {
        #region Properties & Variables
        private Status _status;
        private string _group;
        private string _path;
        #endregion // Properties

        private ScriptBuilder() { }
        #region Methods
        public static ScriptBuilder Empty() => new();

        public ScriptBuilder WithStatus(Status status)
        {
            _status = status;
            return this;
        }

        public ScriptBuilder WithGroup(string group)
        {
            _group = group;
            return this;
        }

        public ScriptBuilder WithPath(string path)
        {
            _path = path;
            return this;
        }
        #endregion

        public Script Build()
        {
            var _script = new Script
            {
                Status = _status,
                Group = _group,
                Path = _path
            };
            return _script;
        }
    }
}