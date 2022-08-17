namespace UIFramework
{
    /// <summary>
    /// Box UI容器
    /// 用于小窗口，可以随时打开和关闭，当打开新Panel时关闭所有Box
    /// </summary>
    public abstract class AbstractBox : AbstractUIContainer
    {
        protected override void OnInit() { }

        protected override void OnOpen() { }

        protected override void OnClose() { }
    }
}
