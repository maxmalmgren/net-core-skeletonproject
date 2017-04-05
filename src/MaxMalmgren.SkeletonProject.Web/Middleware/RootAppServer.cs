using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxMalmgren.SkeletonProject.Web.Middleware
{
    public class RootAppServer
    {
        private readonly RequestDelegate _next;

        private readonly string rootHtmlFileContents;

        public RootAppServer(RequestDelegate next, IHostingEnvironment environment)
        {
            var rootHtmlFilePath = Path.Combine(environment.ContentRootPath, "wwwroot", "static", "root-template.html");
            var generatedFilesFolder = Path.Combine(environment.ContentRootPath, "wwwroot", "static", "gen");
            
            if (!File.Exists(rootHtmlFilePath))
            {
                throw new InvalidOperationException("Root html file template missing");
            }

            if (!Directory.Exists(generatedFilesFolder))
            {
                throw new InvalidOperationException("Generated files folder missing");
            }            

            var scripts = new[] { "bundle" };
            var styles = new[] { "styles" };

            var resolvedScripts = ResolveGeneratedFiles(generatedFilesFolder, scripts, "js");
            var resolvedStylesheets = ResolveGeneratedFiles(generatedFilesFolder, styles, "css");

            rootHtmlFileContents = GenerateRootHtmlFile(new StringBuilder(File.ReadAllText(rootHtmlFilePath)), resolvedScripts, resolvedStylesheets);

            _next = next;
        }

        private string GenerateRootHtmlFile(StringBuilder template, IEnumerable<string> resolvedScripts, IEnumerable<string> resolvedStylesheets)
        {
            var scripts = new StringBuilder();
            var styles = new StringBuilder();

            foreach(var script in resolvedScripts)
            {
                scripts.AppendLine($"<script type=\"text/javascript\" src=\"{script}\"></script>");
            }

            foreach (var style in resolvedStylesheets)
            {
                scripts.AppendLine($"<link type=\"text/css\" href=\"{style}\" rel=\"stylesheet\"></link>");
            }

            template.Replace("[Scripts]", scripts.ToString());
            template.Replace("[Styles]", styles.ToString());

            return template.ToString();
        }

        public static IEnumerable<string> ResolveGeneratedFiles(string generatedFilesFolder, string[] files, string ext)
        {
            foreach(var file in files)
            {
                var exactMatch = Directory.GetFiles(generatedFilesFolder, $"{file}.{ext}").Select(x => new FileInfo(x));

                if (exactMatch.Any())
                {
                    yield return FormatResolvedGeneratedFile(exactMatch.First().Name);
                    continue;
                }

                var otherMatches = Directory.GetFiles(generatedFilesFolder, $"{file}.*.{ext}").Select(x => new FileInfo(x)).OrderByDescending(x => x.LastWriteTime).ToList();

                if (!otherMatches.Any())
                    throw new InvalidOperationException("Could not resolve script dependency: " + file);

                yield return FormatResolvedGeneratedFile(otherMatches.First().Name);
            }
        }

        private static string FormatResolvedGeneratedFile(string file)
        {
            return $"/static/gen/{file}";
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments(new PathString("/api")) || context.Request.Path.StartsWithSegments(new PathString("/static")))
            {
                return _next(context);
            }
            else
            {
                context.Response.Headers.Add("Content-Type", new StringValues("text/html"));
                context.Response.Headers.Add("Cache-Control", new StringValues("no-cache, no-store, must-revalidate"));
                context.Response.Headers.Add("Pragma", new StringValues("no-cache"));
                context.Response.Headers.Add("Expires", new StringValues("0"));

                return context.Response.WriteAsync(rootHtmlFileContents);
            }
        }
    }
}