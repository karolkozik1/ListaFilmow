using Common.CommonData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Students;

public class GetStudentsPagedRequest : PagedRequest
{
    public string? LastNameOrAlbum { get; set; }
    public bool? HideInactive { get; set; }
}
