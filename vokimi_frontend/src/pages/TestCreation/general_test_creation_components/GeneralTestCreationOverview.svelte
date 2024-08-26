<script lang="ts">
    import { Router, Route } from "svelte-routing";
    import SharedTestCreationRoutes from "../templates_shared_components/SharedTestCreationRoutes.svelte";
    import GeneralTestQuestionsView from "./general_test_overview_tabs/questions_tab/GeneralTestQuestionsView.svelte";
    import GeneralTestResultsView from "./general_test_overview_tabs/results_tab/GeneralTestResultsView.svelte";

    export let basepath: string;
</script>

<Router {basepath}>
    <SharedTestCreationRoutes
        mainInfoPath="main-info-view"
        viewConclusionPath="view-conclusion"
        viewStylesPath="view-styles"
    />
    <Route path="view-questions" component={GeneralTestQuestionsView} />
    <Route path="view-results" component={GeneralTestResultsView} />
    <Route>An error has occured. Please go to a previous page</Route>
</Router>
