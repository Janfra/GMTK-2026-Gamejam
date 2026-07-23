namespace GMTK
{
    public interface ISelectable
    {
        public bool IsSelected { get; }
        public void Select();
        public void Deselect();
        public void OnFocusLost(); // Not the most recent clicked
    }
}
