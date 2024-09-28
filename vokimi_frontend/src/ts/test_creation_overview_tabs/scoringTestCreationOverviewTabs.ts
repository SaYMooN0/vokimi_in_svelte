import ResultIconSvg from '../../pages/TestCreation/tab_links_icons/ResultIconSvg.svelte';
import { SharedTestCreationOverviewTabs } from './sharedTestCreationOverviewTabs';

export const ScoringTestCreationOverviewTabs = {
    "Main Info": SharedTestCreationOverviewTabs['Main Info'],
    Styles: SharedTestCreationOverviewTabs.Styles,
    Conclusion: SharedTestCreationOverviewTabs.Conclusion,
    Results: {
        id: "view-results",
        icon: ResultIconSvg,
    },
    Tags: SharedTestCreationOverviewTabs.Tags,
    Publish: SharedTestCreationOverviewTabs.Publish
};
