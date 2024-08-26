<script lang="ts">
    import { Router, Route } from "svelte-routing";
    import SharedTestCreationRoutes from "../templates_shared_components/SharedTestCreationRoutes.svelte";
    export let basepath: string;
</script>

<Router {basepath}>
    <SharedTestCreationRoutes
        mainInfoPath="main-info-view"
        viewStylesPath="view-styles"
        viewConclusionPath="view-conclusion"
    />
    <Route>An error has occured. Please go to a previous page</Route>
</Router>
