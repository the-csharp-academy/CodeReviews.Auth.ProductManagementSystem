using Web.Models.Staff.Items;

namespace Web.Models.Admin;

public class AdminIndex
{
    public IList<StaffItem> Staff { get; set; } = [];
}
