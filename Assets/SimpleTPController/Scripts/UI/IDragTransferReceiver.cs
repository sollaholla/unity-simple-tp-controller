namespace ThirdPersonController.UI
{
    public interface IDragTransferReceiver<T>
    {
        void OnReceiveDrag(UIDragTransferHandler<T> sender, T data);
    }
}