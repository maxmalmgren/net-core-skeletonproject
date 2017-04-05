<template>
    <div>
        <nav id="main-nav">
            <ul>
                <li class="landing-page-link">
                    <router-link to="/"><span>SkeletonProject</span></router-link>
                </li>
                <li class="inner-nav">
                    <ul>
                        <li>
                            <router-link to="/example">
                                <button class="btn btn-primary" type="submit">Example</button>
                            </router-link>
                        </li>
                    </ul>
                </li>
            </ul>            
        </nav>        
        <div>
                <router-view id="main-content"></router-view>           
        </div>        
    </div>
</template>

<script>
    export default {
        data: function() {
            return { };
        }
    };
</script>

<style lang="scss">
    @mixin border-radius($radius) {
        -webkit-border-radius: $radius;
        -moz-border-radius: $radius;
        border-radius: $radius;
    }

    @mixin gradient-background($direction, $start-color, $stop-color) {
        background: -webkit-linear-gradient($direction, $start-color, $stop-color); /* For Safari 5.1 to 6.0 */
        background: -o-linear-gradient($direction, $start-color, $stop-color); /* For Opera 11.1 to 12.0 */
        background: -moz-linear-gradient($direction, $start-color, $stop-color); /* For Firefox 3.6 to 15 */
        background: linear-gradient($direction, $start-color, $stop-color); /* Standard syntax */
    }

    html {
        height: 100%;
    }

    body {
        @include gradient-background(to bottom, #3E606F, #193441);
        min-height: 100%;
        h1, h2 {
            color: white;
        } 
    }

    #main-content {
        max-width:720px;
        margin:auto;
    }
    
    #main-nav {
        > ul {
               padding: 20px 0 20px 0;
               max-width:1080px;
               margin:auto;
               list-style:none;
               display:flex;
               justify-content:space-between;
        }
        li {
            vertical-align:middle;
            display: inline;
        }

        .landing-page-link {
            font-size:2.5em;
            display:inline-flex;
            a, a:active {
                text-decoration: none;
            }
            span {
                font-size:0.6em;
                margin-left: 5px;
            }
        }

        a {
            color: black;
        }

        .inner-nav {
            justify-content:center;
            display:flex;
            align-items:center;

                ul {
                    display: inline-block;

                    li {
                           margin:0 15px 0 15px;
                    }
                }
          
        }
    }
</style>