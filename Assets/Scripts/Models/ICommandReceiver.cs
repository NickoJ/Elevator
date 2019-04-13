namespace Klyukay.Lift.Models
{
    public interface ICommandReceiver
    {

        void AddCommand(in LiftCommand command);

    }
}