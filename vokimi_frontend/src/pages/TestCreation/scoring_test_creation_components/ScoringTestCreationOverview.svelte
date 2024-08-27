<script lang="ts">
    import { Router, Route } from "svelte-routing";
    import SharedTestCreationRoutes from "../templates_shared_components/SharedTestCreationRoutes.svelte";
    import type { TestCreationMainInfoTabData } from "../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationMainInfoTabData";
    export let basepath: string;
    export let testId: string;
    let mainInfoTabData: TestCreationMainInfoTabData | null = null;
</script>

<Router {basepath}>
    <SharedTestCreationRoutes
        {testId}
        mainInfoPath="main-info-view"
        mainInfoData={mainInfoTabData}
        viewStylesPath="view-styles"
        viewConclusionPath="view-conclusion"
    />
    <Route>An error has occured. Please go to a previous page</Route>
</Router>
