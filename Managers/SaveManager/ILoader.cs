/// <summary>
/// Internal interface for loading saved data.
/// </summary>
namespace GameLib.Managers.SaveManager
{
    internal interface ILoader
    {
        /// <summary>
        /// Loads saved data.
        /// </summary>
        public void Load();
    }
}
