<script lang="ts">
    import DraftTestMainInfoView from "./main_info_tab/DraftTestMainInfoView.svelte";
    import DraftTestConclusionView from "./conclusion_tab/DraftTestConclusionView.svelte";
    import DraftTestStylesView from "./styles_tab/DraftTestStylesView.svelte";
    import DraftTestTagsView from "./tags_tab/DraftTestTagsView.svelte";
    import { Route } from "svelte-routing";
    import type { TestCreationMainInfoTabData } from "../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationMainInfoTabData";

    export let testId: string;

    export let mainInfoPath: string;
    export let mainInfoData: TestCreationMainInfoTabData;

    export let viewConclusionPath: string;

    export let viewStylesPath: string;

    export let viewTagsPath: string;
</script>

<Route path={mainInfoPath}>
    <DraftTestMainInfoView {mainInfoData} {testId} />
</Route>
<Route path={viewConclusionPath}>
    <DraftTestConclusionView />
</Route>
<Route path={viewStylesPath}>
    <DraftTestStylesView />
</Route>
<Route path={viewTagsPath}>
    <DraftTestTagsView />
</Route>
