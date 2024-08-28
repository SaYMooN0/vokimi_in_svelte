<script lang="ts">
    import { Router, Route } from "svelte-routing";
    import SharedTestCreationRoutes from "../templates_shared_components/SharedTestCreationRoutes.svelte";
    import GeneralTestQuestionsView from "./general_test_overview_tabs/questions_tab/GeneralTestQuestionsView.svelte";
    import GeneralTestResultsView from "./general_test_overview_tabs/results_tab/GeneralTestResultsView.svelte";
    import { TestCreationMainInfoTabData } from "../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationMainInfoTabData";

    export let basepath: string;
    export let testId: string;

    let mainInfoTabData: TestCreationMainInfoTabData =
        TestCreationMainInfoTabData.empty();
</script>

<Router {basepath}>
    <SharedTestCreationRoutes
        {testId}
        mainInfoPath="main-info-view"
        mainInfoData={mainInfoTabData}
        viewConclusionPath="view-conclusion"
        viewStylesPath="view-styles"
        viewTagsPath="view-tags"
    />
    <Route path="view-questions" component={GeneralTestQuestionsView} />
    <Route path="view-results" component={GeneralTestResultsView} />
    <Route>
        <p class="go-back-error-message">
            An error has occured. Please go to a previous page
        </p>
    </Route>
</Router>
