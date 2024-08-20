<script lang="ts">
    import type { PingAuthResponse } from "../../ts/PingAuthResponse";

    export let authData: PingAuthResponse;
</script>

<p>{authData.Username}!</p>
<p>{authData.Email}!</p>
<p>{authData.UserId}!</p>

