<script lang="ts">
    import AuthorizeView from "../../components/AuthorizeView.svelte";
    import MyTestsTabsContainer from "./MyTestsTabsContainer.svelte";
</script>

<AuthorizeView>
    <div slot="loading">
        <span>Checking Authentication</span>
    </div>
    <div slot="authenticated"  let:authData>
        <MyTestsTabsContainer {authData} />
    </div>
    <div slot="unauthenticated">
        <p>You have to be authenticated to access this page</p>
    </div>
</AuthorizeView>
