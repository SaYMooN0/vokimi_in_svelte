<script lang="ts">
    import { Router, Route } from "svelte-routing";
    import SharedTestCreationRoutes from "../templates_shared_components/SharedTestCreationRoutes.svelte";
    import type { TestCreationTagsTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationTagsTabData";
    import { TestCreationMainInfoTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationMainInfoTabData";
    import { TestCreationStylesTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationStylesTabData";
    import type { TestCreationConclusionTabData } from "../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/test_creation_shared/TestCreationConclusionTabData";

    export let basepath: string;
    export let testId: string;
    export let updateTestName: (name: string) => void;

    let mainInfoTabData: TestCreationMainInfoTabData =
        TestCreationMainInfoTabData.empty();
    let conclusionTabData: TestCreationConclusionTabData;
    let stylesTabData: TestCreationStylesTabData;
    let tagsTabData: TestCreationTagsTabData;
</script>

<Router {basepath}>
    <SharedTestCreationRoutes
        {testId}
        {updateTestName}
        mainInfoTabPath="main-info-view"
        bind:mainInfoTabData
        conclusionTabPath="view-conclusion"
        bind:conclusionTabData
        stylesTabPath="view-styles"
        bind:stylesTabData
        tagsTabPath="view-tags"
        bind:tagsTabData
        publishingTabPath="view-publish"
    />
    <Route>An error has occured. Please go to a previous page</Route>
</Router>
