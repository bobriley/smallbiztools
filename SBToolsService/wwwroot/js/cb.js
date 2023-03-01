
//import jQuery from "./jquery-3.6.0.min.js"
//window.$ = window.jQuery = jQuery;

//import "./jquery-3.6.3.min.js"

//const $ = jQuery;

import { WSHelper, SmallBusinessInfo } from "/js/ws.js";

const myModal = new bootstrap.Modal(document.getElementById('confirmModal'));


function onCancel() {
    // Reset wizard
    $('#smartwizard').smartWizard("reset");

    // Reset form
    document.getElementById("form-1").reset();
    document.getElementById("form-2").reset();
    document.getElementById("form-3").reset();
    document.getElementById("form-4").reset();
}

function onConfirm() {
    let form = document.getElementById('form-4');
    if (form) {
        if (!form.checkValidity()) {
            form.classList.add('was-validated');
            $('#smartwizard').smartWizard("setState", [3], 'error');
            $("#smartwizard").smartWizard('fixHeight');
            return false;
        }

        $('#smartwizard').smartWizard("unsetState", [3], 'error');
        myModal.show();
        gatherInfoAndSubmitSmallBusinessInfo();
    }
}

function closeModal(reset) {
    if (reset) {
        // Reset wizard
        $('#smartwizard').smartWizard("reset");

        // Reset form
        document.getElementById("form-1").reset();
        document.getElementById("form-2").reset();
        document.getElementById("form-3").reset();
        document.getElementById("form-4").reset();
    }

    myModal.hide();
}

async function gatherInfoAndSubmitSmallBusinessInfo() {

    var sbi = new SmallBusinessInfo();   

    sbi.name = $("#business-name").val();
    sbi.sales = $("#sales").val();

    sbi.ownerSalary = $("#owner-salary").val();
    sbi.depreciation = $("#depreciation").val();
    sbi.interest = $("#interest").val();
    sbi.ownerPersonalExpenses = $("#owner-personal-expenses").val();
    
    
    sbi.utilities = $("#utilities").val();
    sbi.rent = $("#rent").val();
    sbi.payroll = $("#payroll").val();
    sbi.miscExpensese = $("#misc-expenses").val();

    sbi.sdeMultiple = $("#sde-multiple").val();
    sbi.sellableInventory = $("#sellable-inventory").val();
    sbi.askingPrice = $("#asking-price").val();

    try {
        var reportData = await WSHelper.submitSmallBusinessInfo(sbi);

        displayReport(reportData);
    } catch (error) {
        console.log("error when trying to submit small business info");
        console.log(error);
    }    
}

function displayReport(reportData) {

    let reportTitleHtml = `Valuation Report for ${reportData.smallBusinessInfo.name}`;

    $("#modal-report-label").html(reportTitleHtml);

    let reportHtml = `
                      <div class="row">
                        <div class="col">
                          <h4 class="mt-3">SDE<i class="st-tooltip bi bi-question-circle-fill" data-toggle="tooltip" title="Seller's Discretionary Earnings"></i></h4>
                          <hr class="my-2">
                          <span>$${reportData.sde.toLocaleString('en-US')}</span>
                        </div>
                        <div class="col">
                          <h4 class="mt-3">Health Ratio<i class="st-tooltip bi bi-question-circle-fill" data-toggle="tooltip" title="Ratio of rent to revenue"></i></h4>
                          <hr class="my-2">           
                          <span>${reportData.healthRatio * 100}%</span>
                        </div>
                      </div> 
                    </div>                      
                    <div class="row">
                        <div class="col">
                          <h4 class="mt-3">Val</h4>
                          <hr class="my-2">
                          <span>$${reportData.sdeValuation.toLocaleString('en-US') }</span>
                        </div>
                        <div class="col">
                          <h4 class="mt-3">Price Delta</h4>
                          <hr class="my-2">
                          <span>$${reportData.priceDelta.toLocaleString('en-US') }</span>
                        </div>
                    </div>
                      `;

    $("#report-body").html(reportHtml);

    $('[data-toggle="tooltip"]').tooltip();
}

$(function () {   
    // Leave step event is used for validating the forms
    $("#smartwizard").on("leaveStep", function (e, anchorObject, currentStepIdx, nextStepIdx, stepDirection) {
        // Validate only on forward movement
        if (stepDirection == 'forward') {
            let form = document.getElementById('form-' + (currentStepIdx + 1));
            if (form) {
                if (!form.checkValidity()) {
                    form.classList.add('was-validated');
                    $('#smartwizard').smartWizard("setState", [currentStepIdx], 'error');
                    $("#smartwizard").smartWizard('fixHeight');
                    return false;
                }
                $('#smartwizard').smartWizard("unsetState", [currentStepIdx], 'error');
            }
        }
    });

    // Step show event
    $("#smartwizard").on("showStep", function (e, anchorObject, stepIndex, stepDirection, stepPosition) {
        $("#prev-btn").removeClass('disabled').prop('disabled', false);
        $("#next-btn").removeClass('disabled').prop('disabled', false);
        if (stepPosition === 'first') {
            $("#prev-btn").addClass('disabled').prop('disabled', true);
        } else if (stepPosition === 'last') {
            $("#next-btn").addClass('disabled').prop('disabled', true);
        } else {
            $("#prev-btn").removeClass('disabled').prop('disabled', false);
            $("#next-btn").removeClass('disabled').prop('disabled', false);
        }

        // Get step info from Smart Wizard
        let stepInfo = $('#smartwizard').smartWizard("getStepInfo");
        $("#sw-current-step").text(stepInfo.currentStep + 1);
        $("#sw-total-step").text(stepInfo.totalSteps);

        if (stepPosition == 'last') {
            gatherInfoAndSubmitSmallBusinessInfo();
            $("#btnFinish").prop('disabled', false);
        } else {
            $("#btnFinish").prop('disabled', true);
        }

        // Focus first name
        if (stepIndex == 1) {
            setTimeout(() => {
                $('#first-name').focus();
            }, 0);
        }
    });

    // Smart Wizard
    $('#smartwizard').smartWizard({
        selected: 0,
        // autoAdjustHeight: false,
        theme: 'round', // basic, arrows, square, round, dots
        transition: {
            animation: 'fade'
        },
        toolbar: {
            showNextButton: true, // show/hide a Next button
            showPreviousButton: true, // show/hide a Previous button
            position: 'bottom', // none/ top/ both bottom

            extraHtml: `<button class="btn btn-success" id="btnFinish" disabled>Calculate Valuation</button>
                        <button class="btn btn-danger" id="btnCancel">Reset</button>`
        },
        anchor: {
            enableNavigation: true, // Enable/Disable anchor navigation
            enableNavigationAlways: false, // Activates all anchors clickable always
            enableDoneState: true, // Add done state on visited steps
            markPreviousStepsAsDone: true, // When a step selected by url hash, all previous steps are marked done
            unDoneOnBackNavigation: true, // While navigate back, done state will be cleared
            enableDoneStateNavigation: true // Enable/Disable the done state navigation
        },
    });

    $("#state_selector").on("change", function () {
        $('#smartwizard').smartWizard("setState", [$('#step_to_style').val()], $(this).val(), !$('#is_reset').prop("checked"));
        return true;
    });

    $("#style_selector").on("change", function () {
        $('#smartwizard').smartWizard("setStyle", [$('#step_to_style').val()], $(this).val(), !$('#is_reset').prop("checked"));
        return true;
    });

    const btnFinish = document.getElementById("btnFinish");
    const btnCancel = document.getElementById("btnCancel");
    const btnCloseResetModal = document.getElementById("close-reset-modal");
    const btnCloseNoResetModal = document.getElementById("close-noreset-modal");

    btnFinish.addEventListener("click", onConfirm);
    btnCancel.addEventListener("click", onCancel);
    btnCloseResetModal.addEventListener("click", onCancel);
    btnCloseResetModal.addEventListener("click", function(event) { closeModal(true); });
    btnCloseNoResetModal.addEventListener("click", function(event) { closeModal(false); });
});