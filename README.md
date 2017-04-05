Skeleton project template using Vue, WebPack, .Net core and Docker.

The templates and files present here have been anonymised from another project of mine, which is at the time of writing running .Net Core via Docker in a Linux container hosted using Amazon Container Service.
The project uses NPM to install dependencies and webpack, which is used to compile and bundle Bootstrap, Vue and a few other dependencies. Docker support is included and to build an image install Docker for Windows and run docker-build-prod.cmd.
The site uses a dynamically generated html file to load the generated bundle. Since the project uses the history javascript API (to avoid having # in the url), this html file is returned for all requests that doesn't match either /api/* or /static/*. 

Aggressive caching and cachebusting is used for the static files when building production, 

Feel free to use this as a starting point for creating your own application.

Usage:
* Replace MaxMalmgren.SkeletonProject in every file and in every file name with your own project name.
* Replace the information in docker-* scripts in src/MaxMalmgren.SkeletonProject.Web with your own docker repository information / image names
* Run *npm install && npm run build-dev* in src/MaxMalmgren.SkeletonProject.Web to generate client side content
* Build and run web project