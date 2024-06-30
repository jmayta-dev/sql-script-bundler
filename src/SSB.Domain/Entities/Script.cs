using SSB.Domain.Enums;
using SSB.Shared.Extensions;

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
    public required string Path { get; init; }
    public ScriptStatus Status { get; private set; } = ScriptStatus.Unknown;
    public string Description { get; init; }

    public string FileName => Path.Split('/').Last();
    public string Group => Path.Split('/').First();
    public IReadOnlyList<string> Observations =>
        _observations
            .ToHashSet() // removes duplicates
            .ToList()
            .AsReadOnly();
    public string Observation => string.Join(Environment.NewLine, [.. Observations]);
    public string StatusDescription => Status.GetStringValue() ?? string.Empty;
    #endregion

    #region Constructor
    private Script() { }
    #endregion // Constructor

    #region Methods
    public void AddObservation(string? observation)
    {
        if (observation == null) return;
        if (_observations.Contains(observation)) return;
        _observations.Add(observation);
    }

    public void AddObservation(string[]? observations)
    {
        if (observations == null) return;
        _observations.AddRange(observations);
    }

    public void ClearObservations() => _observations.Clear();

    #endregion // Methods

    /// <summary>
    /// Script Builder class
    /// </summary>
    public class ScriptBuilder
    {
        #region Properties & Variables
        private ScriptStatus _status;
        private string _description;
        private string _path;
        private readonly List<string> _observations = [];
        #endregion // Properties

        #region Constructor
        private ScriptBuilder() { }
        #endregion

        #region Methods
        public static ScriptBuilder Empty() => new();

        public ScriptBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public ScriptBuilder WithPath(string path)
        {
            _path = path;
            return this;
        }

        public ScriptBuilder WithStatus(char status)
        {
            _status = (ScriptStatus)status;
            return this;
        }

        public ScriptBuilder WithStatus(string status)
        {
            _status = (ScriptStatus)Convert.ToChar(status);
            return this;
        }

        public ScriptBuilder WithStatus(ScriptStatus status)
        {
            _status = status;
            return this;
        }

        public Script Build()
        {
            var _script = new Script
            {
                Description = _description,
                Path = _path,
                Status = _status
            };
            return _script;
        }
        #endregion
    }
}