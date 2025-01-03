<script lang="ts">
    import BaseDialog from "../../../components/BaseDialog.svelte";
    import CloseButton from "../../../components/shared/CloseButton.svelte";
    import {
        PrivacyValues,
        PrivacyValuesUtils,
    } from "../../../ts/enums/PrivacyValues";

    export let testId: string;
    export let updateParentElementPrivacy: (
        newPrivacyVal: PrivacyValues,
    ) => void;
    export let currentTestPrivacy: PrivacyValues;
    let dialogElement: BaseDialog;
    let testPrivacy: PrivacyValues;
    let errorMessage: string = "";
    export function open() {
        testPrivacy = currentTestPrivacy;
        dialogElement.open();
    }
    async function saveChanges() {
        const bodyData = {
            NewPrivacy: testPrivacy,
        };
        const response = await fetch(
            "/api/manageTest/overall/changeTestPrivacy/" + testId,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(bodyData),
            },
        );
        if (response.ok) {
            const data = await response.json();
            updateParentElementPrivacy(
                PrivacyValuesUtils.fromId(data.NewPrivacy),
            );
            errorMessage = "";
            dialogElement.close();
        } else if (response.status === 400) {
            const data = await response.json();
            dialogElement.setErrorMessage(data.error);
        } else {
            dialogElement.setErrorMessage("Unknown error");
        }
    }
</script>

<BaseDialog dialogId="change-test-privacy-dialog" bind:this={dialogElement}>
    <div class="dialog-content">
        <CloseButton onClose={() => dialogElement.close()} />
    </div>
    <h2 class="change-privacy-h">Change test privacy</h2>
    <p class="current-privacy-p">
        Current privacy: {PrivacyValuesUtils.getFullName(currentTestPrivacy)}>
    </p>
</BaseDialog>
