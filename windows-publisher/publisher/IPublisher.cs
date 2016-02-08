using System.Threading.Tasks;

namespace publisher
{
    public interface IPublisher
    {
        Task Publish(object data);
    }
}