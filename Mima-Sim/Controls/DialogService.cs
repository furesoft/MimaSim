namespace MimaSim.Controls
{
    public static class DialogService
    {
        private static ContentDialog _host;

        public static void SetIsHost(ContentDialog target, bool value)
        {
            if (value)
            {
                _host = target;
            }
        }

        public static bool GetIsHost(ContentDialog target)
        {
            return object.ReferenceEquals(_host, target);
        }

        public static void Open(object content)
        {
            if (_host != null)
            {
                _host.DialogContent = content;
                _host.IsOpened = true;
            }
        }

        public static void Open()
        {
            if (_host != null)
            {
                _host.IsOpened = true;
            }
        }

        public static void Close()
        {
            if (_host != null)
            {
                _host.IsOpened = false;
            }
        }
    }
}