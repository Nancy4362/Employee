class Employees {
    private urlGetData = "/employee/table-data-view";
    private urlAddEmployee = '/employee/add';
    private urlSaveEmployee = '/employee/save';
    private urlDeleteEmployee = '/employee/delete';

    constructor() {
        this.init();
    }
    private init() {
        try {
            this.initTable();
            $('#add_employee').click(() => {
                this.add();
            });
        } catch (e) {
            console.error(e);
        }
    }
    private initTable() {
        try {
            Util.request(this.urlGetData, 'GET', 'html', (response) => {
                $('#employees_list tbody').empty();
                $('#employees_list tbody').append(response);

                $(document).on("click", ".employee-delete", (e) => {

                    const id = $(e.currentTarget).data('id');
                    const data = { id: id };
                    this.delete(data);

                });
            }, function () {
                console.error('Failed to get data. Please try again');
            });
        } catch (e) {
            console.error(e);
        }
    }
    private add() {
        try {
            Util.request(this.urlAddEmployee, 'get', 'html', (response) => {
                $('#employee_form').html(response);
                $('#employee_list').hide();

                this.initForm();

            }), () => {
                console.error('Failed to get employee #T6G352. Please try again');
            }
        } catch (e) {
            console.error(e);
        }
    }
    private delete(data) {
        try {
            if (confirm("Are you sure you want to delete this employee?") == true) {

                Util.request(this.urlDeleteEmployee, 'post', 'json', () => {
                    ($ as any).notify('Employee deleted successfully.');
                    location.reload();
                }, () => {
                    ($ as any).notify('Failed to delete Employee. Please try again');
                }, data);
            }
        } catch (e) {
            console.error(e);
        }
    }
   
    private initForm() {
        try {

            $('#save_form').click(() => {
                this.save();
            });

            $('#close_form').click(() => {
                location.reload(); //Reloads the page to show table
            });

        } catch (e) {
            console.error(e);
        }
    }

    private save() {
        try {

            const employee = this.createEmployee();
            Util.request(this.urlSaveEmployee, 'post', 'json', (response) => {
                if (response != null) {
                    ($ as any).notify(response.message);
                    location.reload();
                } else {
                    ($ as any).notify(response.message);
                    console.error('Failed to get data #T7G985. Please try again.');
                }
            }, () => {
            }, employee);
        } catch (e) {
            console.error(e);
        }
    }

    private createEmployee() {
        try {

            const employee = {
                EmployeeId: $('#employee_id').val(),
                FullName: $('#first_name').val(),
                Position: $('#position').val(),
                Address: $('#address').val(),
                PhoneNumber: $('#PhoneNo').val()
            };
            return employee;
        } catch (e) {
            console.error(e);
        }
    }
}
$(document).ready(function () {
    new Employees();
});