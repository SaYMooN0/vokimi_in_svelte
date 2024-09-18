import { getErrorFromResponse } from "../ErrorResponse";
import { StringUtils } from "./StringUtils";

export namespace ImgUtils {
    export function saveImgApiUrl(fileKey: string): string {
        return `/api/vokimisaveimg/${fileKey}`;
    }
    export function imgUrl(fileKey: string): string {
        return `/api/vokimiimgs/${fileKey}`;
    }
    export function imgUrlWithVersion(path: string): string {
        return `${imgUrl(path)}?v=${StringUtils.randomString(10)}`;
    }
    export async function saveImage(file: File, path: string): Promise<string | Err> {
        const formData = new FormData();
        formData.append("file", file);

        const response = await fetch(
            `/api/saveimg/${path}`,
            {
                method: "POST",
                body: formData,
            },
        );
        if (response.ok) {
            const data = await response.json();
            return data.imgPath;
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("Unexpected server error");
        }
    }
}
