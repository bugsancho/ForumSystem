/// <binding BeforeBuild='default' ProjectOpened='watch' />
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    concat = require('gulp-concat'),
    eventStream = require('event-stream'),
    yargs = require('yargs').argv,
    //uglify = require('gulp-uglify'),
    gulpIf = require('gulp-if'),
    minifyCss = require('gulp-minify-css'),
    minifyHtml = require('gulp-minify-html'),
    angularTemplateCache = require('gulp-angular-templatecache'),
    angularFileSort = require('gulp-angular-filesort'),
    ngAnnotate = require('gulp-ng-annotate'),
    del = require('del');
const uglifyes = require('uglify-es');
const composer = require('gulp-uglify/composer');
const uglify = composer(uglifyes, console);
var merge = require('merge-stream');
var isProduction = false;

var sources = {
    appBase: ['app/*.js'],
    app: [
        "app/**/*.js"
    ],
    templates: [
        "app/**/*.html"

    ],
    libsJs: [
        'node_modules/angular/angular.js',
        'node_modules/moment/min/moment.min.js',
        'node_modules/chart.js/dist/Chart.bundle.min.js',
        'node_modules/angular-animate/angular-animate.min.js',
        'node_modules/@uirouter/angularjs/release/angular-ui-router.min.js',
        'node_modules/angular-touch/angular-touch.min.js',
        'node_modules/angular-moment/angular-moment.min.js',
        'node_modules/angular-chart.js/dist/angular-chart.min.js',
        'node_modules/ui-bootstrap4/dist/ui-bootstrap.js',
        'node_modules/ui-bootstrap4/dist/ui-bootstrap-tpls.js'
    ],
    libsCss: [
        'node_modules/bootstrap/dist/css/bootstrap.min.css'
    ],
    sectionFolders: ['threads', 'auth', 'shared', 'posts', 'statistics']


};

var folders = {
    baseApp: 'app/',
    appBuild: 'dist/app/',
    //templates: 'dist/js/app/',
    //appJsBuild: 'dist/js/app/',
    libsJsBuild: 'dist/libs/js/',
    libsCssBuild: 'dist/libs/css/'
};


fileNames = {
    templatesMinified: 'app.templates.min.js',
    scriptsMinified: 'app.min.js',
    libsJsMinified: 'libs.min.js',
    libsCssMinified: 'libs.min.css'
};

gulp.task('libsJs', function () {
    del.sync([folders.libsJsBuild + '*.js']);
    return gulp.src(sources.libsJs)
        .pipe(gulpIf(isProduction, gulpIf(isNonMinifiedFile, uglify())))
        .pipe(gulpIf(isProduction, concat(fileNames.libsJsMinified)))
        .pipe(angularFileSort())
        .pipe(gulp.dest(folders.libsJsBuild));
});

gulp.task('appJs', function () {
    del.sync([folders.appJsBuild + '**/*.js']);
    return gulp.src(sources.app)
        .pipe(ngAnnotate())
        .pipe(angularFileSort())
        .pipe(gulpIf(isProduction, gulpIf(isNonMinifiedFile, uglify())))
        .pipe(gulpIf(isProduction, concat(fileNames.scriptsMinified)))
        .pipe(gulp.dest(folders.appJsBuild));
});


gulp.task('scripts', function () {
    del.sync([folders.appBuild + '**/*.js']); //Delete the scripts from previous build



    sources.sectionFolders.map(function (folder) { // Traverse all the sections
        return merge(
            gulp.src(folders.baseApp + folder + '/**/*.js')
                .pipe(ngAnnotate())
                .pipe(gulpIf(isProduction, gulpIf(isNonMinifiedFile, uglify()))) //Minify if in production environment and file is not already minified
                .pipe(angularFileSort()), // Order angular modules and their dependencies so that no module is required before it's defined
            gulp.src(folders.baseApp + folder + '/**/*.html')
                .pipe(gulpIf(isProduction, minifyHtml({ empty: true }))) // minify html, preserving empty tags
                .pipe(angularTemplateCache(folder + '-templates.js', { module: 'forumSystem.' + folder })) // Puts all the html templates in an angular module ('forumSystem.{{section}}') that loads the templates into angular's $templateCache
        )
            .pipe(angularFileSort())
            .pipe(gulpIf(isProduction, concat(folder + '.js')))
            .pipe(gulp.dest(folders.appBuild + folder + '/')); // Save the file/s to the build directory
    });

    return gulp.src(sources.appBase) // Process the scripts in the base folder 
        .pipe(ngAnnotate())
        .pipe(angularFileSort())
        .pipe(gulpIf(isProduction, concat(fileNames.scriptsMinified)))
        .pipe(gulp.dest(folders.appBuild));
});


gulp.task('libsCss', function () {
    del.sync([folders.libsCssBuild + '*.css']);
    return gulp.src(sources.libsCss)
        .pipe(gulpIf(isProduction, gulpIf(isNonMinifiedFile, minifyCss())))
        .pipe(gulpIf(isProduction, concat(fileNames.libsCssMinified)))
        .pipe(gulp.dest(folders.libsCssBuild));
});

gulp.task('watch', function () {
    gulp.watch(sources.app, gulp.series('scripts'));
    gulp.watch(sources.templates, gulp.series('scripts'));
    gulp.watch(sources.libsCss, gulp.series('libsCss'));
    gulp.watch(sources.libsJs, gulp.series('libsJs'));
});

gulp.task('default', gulp.series('scripts', 'libsCss', 'libsJs'));


function isNonMinifiedFile(file) {
    var minFileRegex = RegExp(/[-_.]min\.(css|js)$/);
    return !(minFileRegex.test(file.path));
}