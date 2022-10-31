using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{

    public class TitleObejcts
    {
        public int id { get; set; }
        public int rev { get; set; }
        public Fields fields { get; set; }
        public _Links2 _links { get; set; }
        public string url { get; set; }
    }

    public class Fields
    {
        public string SystemAreaPath { get; set; }
        public string SystemTeamProject { get; set; }
        public string SystemIterationPath { get; set; }
        public string SystemWorkItemType { get; set; }
        public string SystemState { get; set; }
        public string SystemReason { get; set; }
        public DateTime SystemCreatedDate { get; set; }
        public SystemCreatedby SystemCreatedBy { get; set; }
        public DateTime SystemChangedDate { get; set; }
        public SystemChangedby SystemChangedBy { get; set; }
        public int SystemCommentCount { get; set; }
        public string SystemTitle { get; set; }
        public string SystemBoardColumn { get; set; }
        public bool SystemBoardColumnDone { get; set; }
        public DateTime MicrosoftVSTSCommonStateChangeDate { get; set; }
        public int MicrosoftVSTSCommonPriority { get; set; }
        public float MicrosoftVSTSCommonStackRank { get; set; }
        public string WEF_C237BA7013F34D8097491759F9D1C283_KanbanColumn { get; set; }
        public bool WEF_C237BA7013F34D8097491759F9D1C283_KanbanColumnDone { get; set; }
        public string SystemDescription { get; set; }
    }

    public class SystemCreatedby
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class _Links
    {
        public Avatar avatar { get; set; }
    }

    public class Avatar
    {
        public string href { get; set; }
    }

    public class SystemChangedby
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links1 _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class _Links1
    {
        public Avatar1 avatar { get; set; }
    }

    public class Avatar1
    {
        public string href { get; set; }
    }

    public class _Links2
    {
        public Self self { get; set; }
        public Workitemupdates workItemUpdates { get; set; }
        public Workitemrevisions workItemRevisions { get; set; }
        public Workitemcomments workItemComments { get; set; }
        public Html html { get; set; }
        public Workitemtype workItemType { get; set; }
        public Fields1 fields { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Workitemupdates
    {
        public string href { get; set; }
    }

    public class Workitemrevisions
    {
        public string href { get; set; }
    }

    public class Workitemcomments
    {
        public string href { get; set; }
    }

    public class Html
    {
        public string href { get; set; }
    }

    public class Workitemtype
    {
        public string href { get; set; }
    }

    public class Fields1
    {
        public string href { get; set; }
    }

}
