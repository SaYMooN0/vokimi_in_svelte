<script lang="ts">
    import DraftTestMainInfoView from "./main_info_tab/DraftTestMainInfoView.svelte";
    import DraftTestConcusionView from "./DraftTestConcusionView.svelte";
    import DraftTestStylesView from "./DraftTestStylesView.svelte";
    import { Route } from "svelte-routing";
    import type { TestCreationMainInfoTabData } from "../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationMainInfoTabData";

    export let testId: string;

    export let mainInfoPath: string;
    export let mainInfoData: TestCreationMainInfoTabData | null;

    export let viewConclusionPath: string;

    export let viewStylesPath: string;
</script>

<Route path={mainInfoPath} component={DraftTestMainInfoView}>
    <DraftTestMainInfoView {mainInfoData} {testId} />
</Route>
<Route path={viewConclusionPath} component={DraftTestConcusionView} />
<Route path={viewStylesPath} component={DraftTestStylesView} />
