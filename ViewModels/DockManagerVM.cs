using Core;
using ExceptionExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace WorkProgMain.ViewModels
{
    public class DockManagerVM : VMBase, IFormsServer
    {
        public static DockManagerVM? Instance;
        public DockManagerVM()
        {
            Instance = this;
            
           //_tools.Add(new ProjectsExplorerVM {ContentId = "IDProjectExplorer", IsVisible=false, });
        }
        public static void Clear()
        {
            Instance?._tools.Clear();
            Instance?._docs.Clear();    
        }
        VMBaseForms? IFormsServer.Contains(string id) => Contains(id);
         VMBaseForms IFormsServer.Add(VMBaseForms vmbase) => Add(vmbase);
        public static VMBaseForms? Contains(string id)
        {
            return (VMBaseForms?)
                    Instance?._tools.FirstOrDefault(t => t.ContentID == id) 
                    ?? 
                    Instance?._docs.FirstOrDefault(t => t.ContentID == id);
        }
        public static VMBaseForms Add(VMBaseForms vmbase)
        {
            if (vmbase is ToolVM t) Instance?._tools.Add(t);
            else if (vmbase is DocumentVM d) Instance?._docs.Add(d);
            return vmbase;
        }

        public void RegisterModelView(string RootContentID, Func<VMBaseForms> RegFunc)
        {
            _RegForm.TryAdd(RootContentID, RegFunc);
        }

        public VMBaseForms AddOrGet(string ContentID)
        {
            var c = Contains(ContentID);
            if (c != null)
            {
//                if (c.IsClosed) c.ResetState();
                return c;
            }
            string rootID = ContentID.Split('.', StringSplitOptions.RemoveEmptyEntries)[0];
            var FormVMGenerator = _RegForm.GetValueOrDefault(rootID);
            if (FormVMGenerator == null)
                throw new FlagsArgumentOutOfRangeException(nameof(ContentID), "FormVMGenerator == null bad ID", ContentID);
            var form = FormVMGenerator();
            form.ContentID = ContentID;
            Add(form);
            return form;
        }

        public void Remove(VMBaseForms RemForm)
        {
           if (RemForm is ToolVM t && Tools.Contains(t)) _tools.Remove(t);
           else if (RemForm is DocumentVM d && Docs.Contains(d)) _docs.Remove(d);
        }

        ObservableCollection<DocumentVM> _docs = new ObservableCollection<DocumentVM>();
        ReadOnlyObservableCollection<DocumentVM> _readonyDocs = null!;
        public ReadOnlyObservableCollection<DocumentVM> Docs
        {
            get
            {
                if (_readonyDocs == null)
                    _readonyDocs = new ReadOnlyObservableCollection<DocumentVM>(_docs);

                return _readonyDocs;
            }
        }
        Dictionary<string, Func<VMBaseForms>> _RegForm = new ();
        ObservableCollection<ToolVM> _tools = new ObservableCollection<ToolVM>();
        ReadOnlyObservableCollection<ToolVM> _readonyTools = null!;
        public ReadOnlyObservableCollection<ToolVM> Tools
        {
            get
            {
                if (_readonyTools == null)
                    _readonyTools = new ReadOnlyObservableCollection<ToolVM>(_tools);

                return _readonyTools;
            }
        }
        public IEnumerable<ToolVM> Hiddens => Tools.Where(t => !t.IsVisible/* && !t.IsClosed*/);
        #region ActiveDocument

        private VMBaseForms _activeDocument = null!;
        public VMBaseForms ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                if (_activeDocument != value)
                {
                    _activeDocument = value;

                    var l = ServiceProvider.GetRequiredService<ILogger<VMBaseForms>>();
                    l.LogInformation(" ActiveDocument {} ", _activeDocument?.ContentID);

                    OnPropertyChanged(nameof(ActiveDocument));
                    if (ActiveDocumentChanged != null)
                        ActiveDocumentChanged(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler? ActiveDocumentChanged;

        #endregion

    }
}
