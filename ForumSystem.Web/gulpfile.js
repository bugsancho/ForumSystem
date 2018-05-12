/// <binding ProjectOpened='watch' />
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    concat = require('gulp-concat'),
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
var isProduction = false;

var sources = {
    app: [
        "app/**/*.js"
    ],
    libsJs: [
        'node_modules/angular/angular.min.js',
        'node_modules/moment/min/moment.min.js',
        'node_modules/angular-animate/angular-animate.min.js',
        'node_modules/angular-touch/angular-touch.min.js',
        'node_modules/angular-moment/angular-moment.min.js',
        'node_modules/ui-bootstrap4/dist/ui-bootstrap.js',
        'node_modules/ui-bootstrap4/dist/ui-bootstrap-tpls.js'
    ],
    libsCss: [
        'node_modules/bootstrap/dist/css/bootstrap.min.css'
    ]
};

var folders = {
    appJsBuild: 'dist/js/app/',
    libsJsBuild: 'dist/js/libs/',
    libsCssBuild: 'dist/css/libs/'
};

gulp.task('default', function () {
    // place code for your default task here
});

fileNames = {
    scriptsMinified: 'app.min.js',
    libsJsMinified: 'libs.min.js',
    libsCsssMinified: 'libs.min.css'
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

gulp.task('libsCss', function () {
    del.sync([folders.libsCssBuild + '*.css']);
    return gulp.src(sources.libsCss)
        .pipe(gulpIf(isProduction, gulpIf(isNonMinifiedFile, minifyCss())))
        .pipe(gulpIf(isProduction, concat(fileNames.libsCssMinified)))
        .pipe(gulp.dest(folders.libsCssBuild));
});

gulp.task('watch', function () {
    gulp.watch(sources.libsCss, gulp.series('libsCss'));
    gulp.watch(sources.libsJs, gulp.series('libsJs'));
    gulp.watch(sources.app, gulp.series('appJs'));
});

function isNonMinifiedFile(file) {
    var minFileRegex = RegExp(/[-_.]min\.(css|js)$/);
    return !(minFileRegex.test(file.path));
}