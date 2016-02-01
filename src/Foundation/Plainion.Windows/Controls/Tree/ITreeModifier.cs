
namespace Plainion.Windows.Controls.Tree
{
    public interface ITreeModifier
    {
        void AddChild(object parent);

        void Delete(object node);

        void SetInEditMode(object node, bool editMode);
    }
}
