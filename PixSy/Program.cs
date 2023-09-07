namespace PixSy {
    internal static class Program {
        public static PixSyApp Instance { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Instance = new PixSyApp();
            Instance.Run();
        }
    }
}