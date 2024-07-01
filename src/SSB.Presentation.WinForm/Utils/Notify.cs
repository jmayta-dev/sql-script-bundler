using Microsoft.Extensions.Logging;
using SSB.Shared.Abstractions;

namespace SSB.Presentation.WinForm.Utils;

public static class Notify
{
    public static void Information(ILogger logger, string text, string caption)
    {
        logger.LogInformation("{Message}", text);
        MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static void Error(ILogger logger, Error error)
    {
        logger.LogError("{Message}", error.Description);
        MessageBox.Show(error.Description, error.Code,
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static void Warning(ILogger logger, Error error)
    {
        logger.LogWarning("{Message}", error.Description);
        MessageBox.Show(error.Description, error.Code,
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
