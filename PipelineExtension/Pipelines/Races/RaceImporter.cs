using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json.Linq;
using PipelineExtension.IntermediateFiles;

namespace PipelineExtension
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>

    [ContentImporter(".race", DisplayName = "Race Importer", DefaultProcessor = "RaceProcessor")]
    public class RaceImporter : ContentImporter<RaceIntermediateText>
    {
        public override RaceIntermediateText Import(string filename, ContentImporterContext context)
        {
                return ((JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(File.ReadAllText(filename))).ToObject<RaceIntermediateText>();
        }
    }

}
